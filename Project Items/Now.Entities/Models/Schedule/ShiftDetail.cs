using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Entities.Core;

namespace Now.Entities.Models.Schedule
{
    public class ShiftDetail : BaseEntity, IIdentifiableEntity, IConsumptionEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ShiftDetailId")]
        public Guid ShiftDetailId { get; set; }

        [Display(Name = "ShiftId")]
        public Guid ShiftId { get; set; }

        [Display(Name = "Name")]
        public string ShiftDetailName { get; set; }

        [Display(Name = "Desription")]
        public string ShiftDescription { get; set; }

        [Display(Name = "Start Time")]
        public DateTimeOffset StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTimeOffset EndTime { get; set; }

        [Display(Name = "Duration")]
        public DateTimeOffset Duration { get; set; }

        [Display(Name = "Start Range")]
        public DateTimeOffset StartRange { get; set; }

        [Display(Name = "End Range")]
        public DateTimeOffset EndRange { get; set; }

        [Display(Name = "Shifts")]
        public virtual Shift Shifts { get; set; }

        [Display(Name = "Types")]
        public virtual ShiftDetailType Types { get; set; }

        public virtual Guid EntityId
        {
            get => ShiftDetailId;
            set => ShiftDetailId = value;
        }
    }
}