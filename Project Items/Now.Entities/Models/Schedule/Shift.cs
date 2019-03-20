using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Entities.Core;

namespace Now.Entities.Models.Schedule
{
    public class Shift : BaseEntity, IIdentifiableEntity, IConsumptionEntity
    {
        public Shift()
        {
            ShiftDetails = new List<ShiftDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Guid ShiftId { get; set; }

        [Required]
        [Display(Name = "SiteId")]
        public Guid SiteId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string ShiftName { get; set; }

        [Display(Name = "Description")]
        public string ShiftDescription { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public DateTimeOffset StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public DateTimeOffset EndTime { get; set; }

        [Display(Name = "Duration")]
        public long Ticks { get; set; }

        public virtual ICollection<ShiftDetail> ShiftDetails { get; set; }

        public virtual Guid EntityId
        {
            get => ShiftId;
            set => ShiftId = value;
        }

        [NotMapped]
        public TimeSpan Duration
        {
            get => new TimeSpan(Ticks);
            set => Ticks = value.Ticks;
        }
    }
}