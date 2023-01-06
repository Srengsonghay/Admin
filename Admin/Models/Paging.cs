using Microsoft.EntityFrameworkCore;

namespace Admin.Models
{
    public class Paging<T> : List<T>
    {
        public int TotalItem { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public Paging()
        {

        }
        public Paging(int totalItem, int page, int pageSize = 10)
        {
            int totalPage = (int)Math.Ceiling((decimal)TotalItem / (decimal)PageSize);
            int currentPage = page;
            int startPage = currentPage - 5;
            int endPage = currentPage + 4;
            if(StartPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }
            if(EndPage > TotalPage)
            {
                endPage = TotalPage;
                if(endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }
            TotalPage = totalPage; 
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalItem = totalItem;
            StartPage = startPage;
            EndPage = endPage;
        }


    }
}
