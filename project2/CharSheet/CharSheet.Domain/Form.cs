using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CharSheet.Domain
{
    public class Form
    {
        [Key]
        public Guid FormId { get; set; }

        [ForeignKey("Template")]
        public Guid TemplateId { get; set; }

        // [ForeignKey("FormStyle")]
        // public Guid FormStyleId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }
    }
}