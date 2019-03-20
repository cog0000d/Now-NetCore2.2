using System;
using System.ComponentModel.DataAnnotations;
using Common.Entities.Core;

namespace Now.Entities.Models.Record
{
    public class Sheet : BaseEntity, IIdentifiableEntity, IConsumptionEntity
    {
        [Display(Name = "Id")]
        public Guid ScheduleId { get; set; }

        [Display(Name = "Name")]
        public Guid ShiftId { get; set; }

        [Display(Name = "Employee")]
        public string Employee { get; set; }

        [Display(Name = "Start Time")]
        public DateTimeOffset StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTimeOffset EndTime { get; set; }

        public virtual Guid EntityId
        {
            get => ShiftId;
            set => ShiftId = value;
        }
    }
}