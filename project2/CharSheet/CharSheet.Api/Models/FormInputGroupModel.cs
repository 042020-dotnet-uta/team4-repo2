using System;
using System.Collections.Generic;

namespace CharSheet.Api.Models
{
    public class FormInputGroupModel
    {
        public Guid FormInputGroupId { get; set; }
        public FormTemplateModel FormTemplate { get; set; }
        public IEnumerable<string> FormInputs { get; set; }
    }
}