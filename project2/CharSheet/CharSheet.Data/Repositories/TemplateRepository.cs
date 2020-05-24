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
    public class TemplateRepository : GenericRepository<Template>, ITemplateRepository
    {
        public TemplateRepository(CharSheetContext context)
            : base(context)
        { }

        public async Task<IEnumerable<FormTemplate>> GetFormTemplates(object id)
        {
            return await _context.FormTemplates.Where(FormTemplate => FormTemplate.TemplateId == (Guid) id).ToListAsync();
        }
    }
}