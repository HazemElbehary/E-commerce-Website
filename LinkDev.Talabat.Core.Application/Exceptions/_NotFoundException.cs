namespace LinkDev.Talabat.Core.Application.Exceptions
{
	public class _NotFoundException : ApplicationException
	{
		public _NotFoundException(string? name, object key) 
			: base($"{name} With ({key}) Is Not Found")
		{
		}
	}
}
