using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioFinalBack.Utilities.Extensions;
using DesafioFinalBack.Utilities.UserContext;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DesafioBackEndWebApi.Middleware
{
    public class PacienteContextoMiddleware : IMiddleware
    {
        private readonly IUserContext _userContext;

        public PacienteContextoMiddleware(IUserContext userContext)
        {
            _userContext = userContext;
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
            var authToken = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(authToken))
            {
                var token = authToken.Replace("Bearer", string.Empty).Trim();
                return new JwtSecurityTokenHandler().ReadJwtToken(token);
            }

            return null;
        }

        private static bool IsAuthenticated(HttpContext context)
        {
            var authToken = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(authToken))
                return (context.User?.Identity?.IsAuthenticated ?? false) || !string.IsNullOrEmpty(authToken);
            else
                return true;
        }

        private void SetUserContext(HttpContext context, JwtSecurityToken securityToken)
        {
            _userContext.RequestId = Guid.NewGuid();
            _userContext.StartDateTime = DateTime.UtcNow;
            _userContext.SourceInfo = new SourceInfo
            {
                IP = context?.Connection?.RemoteIpAddress,
                Data = GetAllHeaders(context)
            };

            if (securityToken != null && securityToken.Claims.Any())
            {
                var userName = securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var roles = securityToken.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                var login = securityToken.Claims.FirstOrDefault(c => c.Type == "login")?.Value;

                _userContext.AddData("userName", userName);
                _userContext.AddData("roles", roles);
                _userContext.AddData("login", login);
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