using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CharSheet.Domain
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        public Login Login { get; set; }

        public ICollection<Template> Templates { get; set; }

        public ICollection<Sheet> Sheets { get; set; }
    }
}
