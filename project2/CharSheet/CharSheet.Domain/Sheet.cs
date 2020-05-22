using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharSheet.Domain
{
    public class Sheet
    {
        [Key]
        public Guid SheetId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("Template")]
        public Guid TemplateId { get; set; }
    }
}