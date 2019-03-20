using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Now.Entities.Models.ViewModels
{
    public class LogsVm
    {
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public DateTimeOffset LogTime { get; set; }
    }
}
