using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Entities.Core;

namespace Now.Entities.Models.Time
{
    public class Log : Entity, IIdentifiableEntity
    {
        public Log()
        {
            LogDetails = new List<LogDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Log ID")]
        public Guid LogId { get; set; }

        [Required]
        [Display(Name = "Source Id")]
        public Guid SourceId { get; set; }

        [Required]
        [Display(Name = "Type")]
        [StringLength(100)]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Reference Id")]
        [StringLength(100)]
        public string Value { get; set; } // Employee Id or Asset Id

        [Required]
        [Display(Name = "Time")]
        public DateTimeOffset Data { get; set; }

        [Required]
        [Display(Name = "Download Date and Time")]
        public DateTimeOffset DownloadDate { get; set; }


        public virtual ICollection<LogDetail> LogDetails { get; set; }

        public virtual Guid EntityId
        {
            get => LogId;
            set => LogId = value;
        }
    }
}