using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.Core.Application.Exceptions;

namespace LinkDev.Talabat.APIs.Controllers.Middlewares
{
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IWebHostEnvironment _environment;
		private readonly ILogger<ExceptionHandlerMiddleware> _logger;

		public ExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment environment, ILogger<ExceptionHandlerMiddleware> logger)
		{
			_next = next;
			_environment = environment;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				if (_environment.IsDevelopment())
				{
					_logger.LogError(ex, ex.Message);
				}
				else
				{
					// Log Error In DB Or In File
				}

				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
		{
			ApiResponce response;
			switch (ex)
			{
				case _NotFoundException:
					response = _environment.IsDevelopment() ?
						new ApiExceptionResponse(404, ex.Message, ex.StackTrace!.ToString())
						: new ApiExceptionResponse(404, ex.Message);
					httpContext.Response.StatusCode = 404;
					httpContext.Response.ContentType = "text/json";
					await httpContext.Response.WriteAsync(response.ToString());
					break;

				case ValidationException validationException:
					response = new ApiValidationErrorResponse(ex.Message)
					{
						Errors = validationException.Errors
					};

					httpContext.Response.StatusCode = 400;
					httpContext.Response.ContentType = "text/json";
					await httpContext.Response.WriteAsync(response.ToString());
					break;

				case BadRequestException:
					response = _environment.IsDevelopment() ?
						new ApiExceptionResponse(400, ex.Message, ex.StackTrace!.ToString())
						: new ApiExceptionResponse(400, ex.Message);
					httpContext.Response.StatusCode = 400;
					httpContext.Response.ContentType = "text/json";
					await httpContext.Response.WriteAsync(response.ToString());
					break;

				case UnAuthorizedException:
					response = new ApiExceptionResponse(401, ex.Message);
					httpContext.Response.StatusCode = 401;
					httpContext.Response.ContentType = "text/json";
					await httpContext.Response.WriteAsync(response.ToString());
					break;

				default:
					response = _environment.IsDevelopment() ?
						new ApiExceptionResponse(500, ex.Message, ex.StackTrace!.ToString())
						: new ApiExceptionResponse(500, ex.Message);
					httpContext.Response.StatusCode = 500;
					httpContext.Response.ContentType = "text/json";
					await httpContext.Response.WriteAsync(response.ToString());
					break;
			}
		}
	}
}