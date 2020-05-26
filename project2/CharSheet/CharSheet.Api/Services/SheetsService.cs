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
        Task<SheetModel> GetSheet(object id);
        Task<FormInputGroupModel> GetFormInputGroup(FormInputGroup formInputGroup);
        Task<FormInputGroupModel> GetFormInputGroup(object id);
    }

    public partial class BusinessService : IBusinessService
    {
        public async Task<SheetModel> GetSheet(object id)
        {
            // Load sheet from database.
            var sheet = await _unitOfWork.SheetRepository.Find(id);
            if (sheet == null)
                throw new InvalidOperationException("Sheet not found");
            var formInputGroups = await _unitOfWork.SheetRepository.GetFormInputGroups(id);

            // Instantiate sheet model.
            var sheetModel = new SheetModel
            {
                SheetId = sheet.SheetId,
                UserId = sheet.UserId,
            };

            // Instantiate a form input group model for each form input group.
            var formInputGroupModels = new List<FormInputGroupModel>();
            foreach (var formInputGroup in formInputGroups)
            {
                // Convert form input group object to model.
                var formInputGroupModel = await GetFormInputGroup(formInputGroup);
                formInputGroupModels.Add(formInputGroupModel);
            }
            sheetModel.FormGroups = formInputGroupModels.AsEnumerable();

            return sheetModel;
        }

        public async Task<FormInputGroupModel> GetFormInputGroup(FormInputGroup formInputGroup)
        {
            // Instantiate form input group model.
            var id = formInputGroup.FormInputGroupId;
            var formInputGroupModel = new FormInputGroupModel
            {
                FormInputGroupId = formInputGroup.FormInputGroupId,

                // Get form template as model.
                FormTemplate = await this.GetFormTemplate(formInputGroup.FormTemplateId)
            };

            // Get form inputs as model.
            var formInputs = await _unitOfWork.FormInputGroupRepository.GetFormInputs(id);
            formInputGroupModel.FormInputs = formInputs.Select(formInput => formInput.Value);
            return formInputGroupModel;
        }

        public async Task<FormInputGroupModel> GetFormInputGroup(object id)
        {
            // Load form input group from database.
            var formInputGroup = await _unitOfWork.FormInputGroupRepository.Find(id);
            if (formInputGroup == null)
                throw new InvalidOperationException("Form input group not found.");
            return await GetFormInputGroup(formInputGroup);
        }
    }
}