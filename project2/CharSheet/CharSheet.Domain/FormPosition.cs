using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharSheet.Domain
{
    public class FormPosition
    {
        [Key]
        public Guid FormPostionId { get; set; }

        [ForeignKey("FormTemplate")]
        public Guid FormTemplateId { get; set; }

        public int OffsetTop { get; set; }
        public int OffsetLeft { get; set; }

        public int XPos { get; set; }
        public int YPos { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}