using System.Text.Json;

namespace LinkDev.Talabat.APIs.Controllers.Errors
{
	public class ApiExceptionResponse : ApiResponce
	{
        public ApiExceptionResponse(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }
        public string? Details { get; set; }

		public override string ToString()
		{
			return JsonSerializer.Serialize(this, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

		}
	}
}
