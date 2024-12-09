namespace LinkDev.Talabat.APIs.Controllers.Errors
{
	public class ApiValidationErrorResponse : ApiResponce
	{
        public required Dictionary<string, string[]> Errors { get; set; }
        public ApiValidationErrorResponse(string? Message = null) : base(400, Message)
        {
        }
    }
}
