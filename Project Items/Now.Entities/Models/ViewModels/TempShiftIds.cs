using System;
using System.Collections.Generic;
using System.Text;

namespace Now.Entities.Models.ViewModels
{
    public class TempShiftIds
    {
        public Guid ShiftId { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public long Ticks { get; set; }
    }
}
