using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CharSheet.Domain;
using CharSheet.Data;
using CharSheet.Api.Models;

namespace CharSheet.Api.Services
{
    public partial interface IBusinessService
    {
        
    }

    public partial class BusinessService : IBusinessService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<BusinessService> _logger;

        public BusinessService(ILogger<BusinessService> logger, CharSheetContext context)
        {
            this._logger = logger;
            this._unitOfWork = new UnitOfWork(context);
        }

        #region Helpers
        public async Task<User> AuthenticateUser(object id)
        {
            var user = await _unitOfWork.UserRepository.Find(id);
            if (user == null)
                throw new InvalidOperationException("User not found.");
            else
                return user;
        }
        #endregion
    }
}