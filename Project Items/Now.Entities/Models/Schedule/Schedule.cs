using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Common.Entities.Core;

namespace Now.Entities.Models.Schedule
{
    public class Schedule : BaseEntity, IIdentifiableEntity, IConsumptionEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ScheduleId")]
        public Guid ScheduleId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public Guid ShiftId { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public string Employee { get; set; }

        [Display(Name = "Start Time")]
        public DateTimeOffset StartRange { get; set; }

        [Display(Name = "Start Time")]
        public DateTimeOffset StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTimeOffset EndTime { get; set; }

        [Display(Name = "End Time")]
        public DateTimeOffset EndRange { get; set; }

        public virtual Guid EntityId
        {
            get => ScheduleId;
            set => ScheduleId = value;
        }

        [NotMapped]
        public Int16 Year
        {
            get => (Int16)StartTime.Year;
            set => StartTime = StartTime;
        }

        [NotMapped]
        public Int16 Month
        {
            get => (Int16)StartTime.Month;
            set => StartTime = StartTime;
        }

        [NotMapped]
        public Int16 Week
        {
            get => (Int16)CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(StartTime.DateTime,CalendarWeekRule.FirstFourDayWeek,DayOfWeek.Monday);
            set => StartTime = StartTime;
        }

        [NotMapped]
        public Int16 Day
        {
            get => (Int16)StartTime.Day;
            set => StartTime = StartTime;
        }


    }
}