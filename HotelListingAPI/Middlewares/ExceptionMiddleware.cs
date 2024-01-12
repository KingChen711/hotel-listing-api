using HotelListingAPI.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace HotelListingAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong while processing {context.Request.Path}");
                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            context.Response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            var errorDetails = new ErrorDetails
            {
                ErrorType = "Failure",
                ErrorMessage = e.Message
            };

            switch (e)
            {
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    errorDetails.ErrorType = "Not Found";
                    break;
                case BadRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    errorDetails.ErrorType = "Bad Request";
                    break;
            }

            var response = JsonConvert.SerializeObject(errorDetails);
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(response);
        }

        public class ErrorDetails
        {
            public required string ErrorType { get; set; }
            public required string ErrorMessage { get; set; }
        }
    }
}
