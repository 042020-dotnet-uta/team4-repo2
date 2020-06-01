using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using CharSheet.Domain;
using CharSheet.Domain.Interfaces;

namespace CharSheet.Data.Repositories
{
    public class FormInputGroupRepository : GenericRepository<FormInputGroup>, IFormInputGroupRepository
    {
        public FormInputGroupRepository(CharSheetContext context)
            : base(context)
        { }

        public async override Task<FormInputGroup> Find(object id)
        {
            return (await this.Get(fig => fig.FormInputGroupId == (Guid) id, null, "FormTemplate,FormTemplate.FormPosition,FormTemplate.FormLabels,FormInputs")).FirstOrDefault();
        }
    }
}