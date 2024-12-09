namespace LinkDev.Talabat.Core.Application.Exceptions
{
	public class ValidationException : ApplicationException
	{
		public required Dictionary<string, string[]> Errors { get; set; }

		public ValidationException(string message = "Validation Exception")
            : base(message)
        { 
        }
    }
}
