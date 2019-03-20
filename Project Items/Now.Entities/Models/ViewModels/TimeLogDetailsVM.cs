using System;
using System.Collections.Generic;
using System.Text;

namespace Now.Entities.Models.ViewModels
{
    public class TimeLogDetailsVM
    {
        public string EmployeeId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        
    }
}
