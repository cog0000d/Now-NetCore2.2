using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Entities.Core;

namespace Now.Entities.Models.Time
{
    public class Source : BaseEntity, IIdentifiableEntity
    {
        public Source()
        {
            Logs = new List<Log>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Guid SourceId { get; set; }

        [Display(Name = "TenantId")]
        public Guid? TenantId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(150)]
        public string SourceName { get; set; }

        [Required]
        [Display(Name = "Desription")]
        [StringLength(4000)]
        public string SourceDescription { get; set; }

        public virtual ICollection<Log> Logs { get; set; }

        public virtual Guid EntityId
        {
            get => SourceId;
            set => SourceId = value;
        }
    }
}