using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Now.Api.Model
{
    public class Param
    {
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public DateTimeOffset DateTime { get; set; }
    }
}
