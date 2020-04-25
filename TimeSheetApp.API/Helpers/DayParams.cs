using System;

namespace TimeSheetApp.API.Helpers
{
    public class DayParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public int UserId { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}