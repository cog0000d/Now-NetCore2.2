using System;
using System.Collections.Generic;
using System.Text;

namespace Now.Entities.Models.ViewModels
{
    public class ScheduleVM
    {
        public Guid ShiftId { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public bool isOpen { get; set; }

        public bool isWholeDay { get; set; }

        public string timeZone { get; set; }
    }
}
