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
    public class SheetRepository : GenericRepository<Sheet>, ISheetRepository
    {
        public SheetRepository(CharSheetContext context)
            : base(context)
        { }
    }
}