using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using Desafio.Repository.Interface;
// using Desafio.Helper.Attributes;
// using Desafio.Helper.Exceptions;
// using Desafio.Helper.Messages;
// using Desafio.Helper.Responses;
using log4net;
using Desafio.Helper.Attributes;
using Desafio.Helper.Responses;
using Desafio.Helper.Exceptions;
using Desafio.Helper.Messages;

namespace DesafioBackEndWebApi.Middleware
{
    public class ApiMiddleware : IMiddleware
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ApiMiddleware));
        private readonly ITransactionManager _transactionManager;
        public ApiMiddleware(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            var transactionRequired = context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata.GetMetadata<RequiredTransactionAttribute>();

            try
            {
                if (transactionRequired != null)
                {
                    await _transactionManager.BeginTransactionAsync(transactionRequired.IsolationLevel);

                    await next.Invoke(context);

                    await _transactionManager.CommitTransactionsAsync();
                }
                else
                {
                    await next.Invoke(context);
                }

                stopwatch.Stop();
                _log.InfoFormat("Service executed successfully: {0} {1} [{2} ms]", context.Request.Method, context.Request.Path, stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                if (transactionRequired != null)
                    await _transactionManager.RollbackTransactionsAsync();
                
                stopwatch.Stop();
                _log.Error($"Service error: {context.Request.Path} / Message: {ex.Message} [{stopwatch.ElapsedMilliseconds}]", ex);
                await HandleException(context, ex);
            }
        }

        private static async Task HandleException(HttpContext context, Exception exception)
        {
            var response = context.Response;

            response.ContentType = "application/json";

            await response.WriteAsync(JsonConvert.SerializeObject(new DefaultResponse(HttpStatusCode.InternalServerError, GetMessages(exception))));
        }

        private static List<string> GetMessages(Exception exception)
        {
            var messages = new List<string>();

            switch (exception)
            {
                case ServiceException:
                    messages.Add(exception.Message);
                    break;
                case ServiceListException:
                    messages = ((ServiceListException)exception).Messages;
                    break;
                default:
                    messages.Add(string.Format(InfraMessages.UnexpectedError));
                    break;
            }

            return messages;
        }
    }
}