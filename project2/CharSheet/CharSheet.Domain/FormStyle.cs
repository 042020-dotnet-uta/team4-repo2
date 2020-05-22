using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharSheet.Domain
{
    public class FormStyle
    {
        public Guid FormStyleId { get; set; }
        public string Type { get; set; }
    }
}