using System;
using System.Collections.Generic;

namespace CharSheet.Api.Models
{
    public class SheetModel
    {
        public Guid SheetId { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<FormInputGroupModel> FormGroups { get; set; }
    }
}