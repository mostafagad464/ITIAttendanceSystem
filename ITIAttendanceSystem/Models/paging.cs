namespace ITIAttendanceSystem.Models
{
    public class paging
    {
		public int TotalItems { get; set; }
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
		public int StartPage { get; set; }
		public int EndPage { get; set; }

		public paging() { }


		public paging(int totalitem, int page, int pagesize = 10)
		{
			int tottalpages = (int)Math.Ceiling((decimal)totalitem / (decimal)pagesize);
			int currentpage = page;
			int startpage = currentpage - 5;
			int endpage = currentpage + 4;
			if (startpage <= 0)
			{

				endpage = endpage - (startpage - 1);
				startpage = 1;
			}
			if (endpage >= tottalpages)
			{

				endpage = tottalpages;
				if (endpage > 10)
				{
					startpage = endpage - 9;
				}
			}
			TotalItems = totalitem;
			CurrentPage = currentpage;
			PageSize = pagesize;
			TotalPages = tottalpages;
			StartPage = startpage;
			EndPage = endpage;





		}
	}
}
