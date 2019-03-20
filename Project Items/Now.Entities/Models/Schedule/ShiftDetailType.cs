using System;
using System.ComponentModel.DataAnnotations;
using Common.Entities.Core;

namespace Now.Entities.Models.Schedule
{
    public class ShiftDetailType : BaseEntity, IIdentifiableEntity, IConsumptionEntity
    {
        [Display(Name = "Id")]
        public Guid ShiftDetailTypeId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Active")]
        public short Active { get; set; }

        public virtual Guid EntityId
        {
            get => ShiftDetailTypeId;
            set => ShiftDetailTypeId = value;
        }
    }
}