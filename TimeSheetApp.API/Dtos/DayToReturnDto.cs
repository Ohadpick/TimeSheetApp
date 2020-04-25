
using System;
using TimeSheetApp.API.Models;

namespace TimeSheetApp.API.Dtos
{
    public class DayToReturnDto
    {
        public DateTime ReportedDate { get; set; }
        public int UserId { get; set; }
        public int DaySequenceId { get; set; } = 1;
        public int ProjectId { get; set; } = 1;
        public Project Project { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Remark { get; set; }
    }
}