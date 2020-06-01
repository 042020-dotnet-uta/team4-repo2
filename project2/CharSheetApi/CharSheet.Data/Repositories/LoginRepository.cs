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
    public class LoginRepository : GenericRepository<Login>, ILoginRepository
    {
        public LoginRepository(CharSheetContext context)
            : base(context)
        { }
    }
}