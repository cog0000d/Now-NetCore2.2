using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Entities.Core;

namespace Now.Entities.Models.Time
{
    public class Type : BaseEntity, IIdentifiableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Guid TypeId { get; set; }

        [Display(Name = "TenantId")]
        public Guid? TenantId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(150)]
        public string TypeName { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(4000)]
        public string TypeDescription { get; set; }

        public virtual Guid EntityId
        {
            get => TypeId;
            set => TypeId = value;
        }
    }
}