using System;

namespace TimeSheetApp.API.Helpers
{
    public class ProjectParams
    {
        private const int MaxPageSize = 500;
        public int PageNumber { get; set; } = 1;
        private int pageSize = MaxPageSize;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
    }
}