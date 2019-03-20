using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Now.Api.Model
{
    public class ProposedShift
    {
        [Required]
        public Guid EmployeeId { get; set; }

        [Required]
        public Guid ShiftId { get; set; }

        [Required]
        public DateTimeOffset dateTime { get; set; }

    }
}
