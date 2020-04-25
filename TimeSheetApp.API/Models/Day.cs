using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TimeSheetApp.API.Models
{
    public class Day
    {
        public DateTime ReportedDate { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public int? DaySequenceId { get; set; } = 1;
        public int? ProjectId { get; set; } = 1;
        public virtual Project Project { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string Remark { get; set; }
    }
}