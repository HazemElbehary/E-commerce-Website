namespace LinkDev.Talabat.Core.Application.Abstraction.Common
{
	public class Paginations<T>(int PageIndex, int PageSize, int Count, IEnumerable<T> Data)
	{
		public int PageIndex { get; set; } = PageIndex;
		public int PageSize { get; set; } = PageSize;
		public int Count { get; set; } = Count;
		public IEnumerable<T> Data { get; set; } = Data;
	}
}
