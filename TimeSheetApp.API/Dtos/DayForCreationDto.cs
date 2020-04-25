using System;
using TimeSheetApp.API.Models;

namespace TimeSheetApp.API.Dtos
{
    public class DayForCreationDto
    {
        public DateTime ReportedDate { get; set; }
        public int UserId { get; set; }
        public int DaySequenceId { get; set; } = 1;
        public int ProjectId { get; set; } = 1;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Remark { get; set; }
    }
}