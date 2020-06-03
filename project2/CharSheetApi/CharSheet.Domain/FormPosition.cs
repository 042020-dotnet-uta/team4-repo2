using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharSheet.Domain
{
    public class FormPosition
    {
        [Key]
        public Guid FormPostionId { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}