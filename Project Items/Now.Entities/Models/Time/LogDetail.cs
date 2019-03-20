using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Entities.Core;

namespace Now.Entities.Models.Time
{
    public class LogDetail : IIdentifiableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Log Details ID")]
        public Guid LogDetailId { get; set; }

        [Required]
        [Display(Name = "Log ID")]
        public Guid LogId { get; set; }

        [Required]
        [Display(Name = "Type")]
        [StringLength(256)]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Value")]
        [StringLength(256)]
        public string Value { get; set; }

        public virtual Log Logs { get; set; }

        public virtual Guid EntityId
        {
            get => LogDetailId;
            set => LogDetailId = value;
        }
    }
}