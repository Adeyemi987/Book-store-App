using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StorBookWebApp.Shared.ErrorResponses;
using System;
using System.Net;
using System.Threading.Tasks;

namespace StorBookWebApp.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = ApiResponse<string>.Fail(error.Message);
                switch (error)
                {
                    case BadRequestException e:
                        // custom application error
                        _logger.LogError(e.StackTrace, e);
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case NotFoundException e:
                        // not found error
                        _logger.LogError(e.StackTrace, e);
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        responseModel.Message = e.Message;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel.Message = "Internal Server Error. Please Try Again Later.";
                        break;
                }
                var result = JsonConvert.SerializeObject(responseModel);
                await response.WriteAsync(result);
            }
        }

    }
}
