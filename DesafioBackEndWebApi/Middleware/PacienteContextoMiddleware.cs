using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Desafio.Helper.PacienteContexto;

namespace DesafioBackEndWebApi.Middleware
{
    public class PacienteContextoMiddleware : IMiddleware
    {
        private readonly IPacienteContexto _pacienteContexto;

        public PacienteContextoMiddleware(IPacienteContexto pacienteContexto)
        {
            _pacienteContexto = pacienteContexto;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (SetPaciente(context))
                await next.Invoke(context);
        }

        private bool SetPaciente(HttpContext context)
        {
            if (IsAuthenticated(context))
            {
                var securityToken = GetSecurityToken(context);

                SetUserContext(context, securityToken);

                return true;
            }
            else
            {
                throw new UnauthorizedAccessException("Paciente não autorizado");
            }
        }

        private static JwtSecurityToken GetSecurityToken(HttpContext context)
        {
            var authToken = context.Request.Headers["Autorização"].ToString();

            if (!string.IsNullOrEmpty(authToken))
            {
                var token = authToken.Replace("Bearer", string.Empty).Trim();
                return new JwtSecurityTokenHandler().ReadJwtToken(token);
            }

            return null;
        }

        private static bool IsAuthenticated(HttpContext context)
        {
            var authToken = context.Request.Headers["Autorização"].ToString();

            if (!string.IsNullOrEmpty(authToken))
                return (context.User?.Identity?.IsAuthenticated ?? false) || !string.IsNullOrEmpty(authToken);
            else
                return true;
        }

        private void SetUserContext(HttpContext context, JwtSecurityToken securityToken)
        {
            _pacienteContexto.RequestId = Guid.NewGuid();
            _pacienteContexto.StartDateTime = DateTime.UtcNow;
            _pacienteContexto.SourceInfo = new SourceInfo
            {
                IP = context?.Connection?.RemoteIpAddress,
                Data = GetAllHeaders(context)
            };

            if (securityToken != null && securityToken.Claims.Any())
            {
                var userName = securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var roles = securityToken.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                var login = securityToken.Claims.FirstOrDefault(c => c.Type == "login")?.Value;

                // _pacienteContexto.AddData("userName", userName);
                // _pacienteContexto.AddData("roles", roles);
                // _pacienteContexto.AddData("login", login);
            }
        }

        private static Hashtable GetAllHeaders(HttpContext context)
        {
            var hashtable = new Hashtable();
            var requestHeaders = context?.Request?.Headers;

            if (requestHeaders == null)
                return hashtable;

            foreach (var header in requestHeaders)
                hashtable.Add(header.Key, header.Value);

            return hashtable;
        }
    }
}