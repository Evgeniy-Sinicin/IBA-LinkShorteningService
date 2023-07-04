

namespace LinkShorteningService.BusinessLogic.DTO
{
	public abstract class PageRequestBase
	{
		public int Page { get; set; }
		public int Size { get; set; }
		public int Skip => (Page - 1) * Size;
	}
}
