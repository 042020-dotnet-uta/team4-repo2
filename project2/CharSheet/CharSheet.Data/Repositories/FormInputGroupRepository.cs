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

        public async Task<IEnumerable<FormInput>> GetFormInputs(object id)
        {
            return await base._context.FormInputs.Where(formInput => formInput.FormInputGroupId == (Guid) id).ToListAsync();
        }
    }
}