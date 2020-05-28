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
        #region GET
        Task<IEnumerable<SheetModel>> GetSheets(object id);
        Task<SheetModel> GetSheet(object id);
        #endregion

        #region POST
        Task<SheetModel> CreateSheet(SheetModel sheetModel);
        #endregion

        #region PUT
        Task<SheetModel> UpdateSheet(SheetModel sheetModel);
        #endregion

        #region DELETE
        Task DeleteSheet(object id);
        #endregion
    }

    public partial class BusinessService : IBusinessService
    {
        #region GET
        public async Task<IEnumerable<SheetModel>> GetSheets(object id)
        {
            // Load sheets from database, filter by user id.
            var sheets = await _unitOfWork.SheetRepository.Get(sheet => sheet.UserId == (Guid)id);

            // Sheets as sheet models.
            var sheetModels = new List<SheetModel>();
            foreach (var sheet in sheets)
            {
                // Sheet as sheet model.
                var sheetModel = await ToModel(sheet);
            }

            return sheetModels.AsEnumerable();
        }

        public async Task<SheetModel> GetSheet(object id)
        {
            // Load sheet from database.
            var sheet = await _unitOfWork.SheetRepository.Find(id);
            if (sheet == null)
                throw new InvalidOperationException("Sheet not found");

            // Returns sheet as sheet model.
            return await ToModel(sheet);
        }
        #endregion

        #region POST
        public async Task<SheetModel> CreateSheet(SheetModel sheetModel)
        {
            await AuthenticateUser(sheetModel.UserId);

            var sheet = new Sheet
            {
                UserId = sheetModel.UserId,
                FormInputGroups = new List<FormInputGroup>()
            };

            // Create form input groups.
            foreach (var formInputGroupModel in sheetModel.FormGroups)
            {
                var formTemplate = await _unitOfWork.FormTemplateRepository.Find(formInputGroupModel.FormTemplateId);
                if (formTemplate == null)
                    throw new InvalidOperationException("Form template not found.");

                var formInputGroup = new FormInputGroup
                {
                    FormTemplate = formTemplate,
                    FormInputs = new List<FormInput>()
                };

                for (int i = 0; i < formInputGroupModel.FormInputs.Count(); i++)
                {
                    var formInput = new FormInput
                    {
                        Index = i,
                        Value = formInputGroupModel.FormInputs.ElementAt(i)
                    };
                    formInputGroup.FormInputs.Add(formInput);
                }

                sheet.FormInputGroups.Add(formInputGroup);
            }

            await _unitOfWork.SheetRepository.Insert(sheet);
            await _unitOfWork.Save();

            _logger.LogInformation($"New Sheet: {sheet.SheetId} by {sheet.SheetId}");
            return await ToModel(sheet);
        }
        #endregion

        #region PUT
        public async Task<SheetModel> UpdateSheet(SheetModel sheetModel)
        {
            var user = await AuthenticateUser(sheetModel.UserId);

            // Load existing sheet from database.
            var sheet = await _unitOfWork.SheetRepository.Find(sheetModel.SheetId);
            if (sheet == null)
                throw new InvalidOperationException("Sheet not found.");
            if (sheet.UserId != sheetModel.UserId)
                throw new InvalidOperationException("User mismatch.");

            // Validate sheet model structure.
            if (sheetModel.FormGroups == null)
                throw new InvalidOperationException("Missing form groups.");
            foreach (var formGroup in sheetModel.FormGroups)
                if (formGroup.FormTemplateId == null)
                    throw new InvalidOperationException("Missing form template id.");

            sheetModel.FormGroups = sheetModel.FormGroups.OrderBy(fg => fg.FormTemplateId);

            var deletedInputs = new List<FormInput>();

            for (int i = 0; i < sheetModel.FormGroups.Count(); i++)
            {
                var formGroup = sheetModel.FormGroups.ElementAt(i);
                var formInputGroup = sheet.FormInputGroups.ElementAt(i);

                // Verify form templates.
                if (formGroup.FormTemplateId != formInputGroup.FormTemplateId)
                    throw new InvalidOperationException("Form template mismatch.");

                int j;
                var formInputsCount = formInputGroup.FormInputs.Count();
                for (j = 0; j < formGroup.FormInputs.Count(); j++)
                {
                    var input = formGroup.FormInputs.ElementAt(j);
                    if (formInputsCount > j)
                    {
                        var formInput = formInputGroup.FormInputs.ElementAt(j);
                        formInput.Value = input;
                    }
                    else
                    {
                        formInputGroup.FormInputs.Add(new FormInput { Index = j, Value = input });
                    }
                }

                // Delete excess form inputs.
                if (j < formInputsCount)
                {
                    deletedInputs.AddRange(formInputGroup.FormInputs.ToList().GetRange(j, formInputsCount - j));
                }
            }

            await _unitOfWork.FormInputRepository.RemoveRange(deletedInputs);
            await _unitOfWork.SheetRepository.Update(sheet);
            await _unitOfWork.Save();

            return await ToModel(sheet);
        }
        #endregion

        #region DELETE
        public async Task DeleteSheet(object id)
        {
            var sheet = await _unitOfWork.SheetRepository.Find(id);
            if (sheet == null)
                throw new InvalidOperationException("Sheet not found.");
            
            await _unitOfWork.SheetRepository.Remove(sheet);
            await _unitOfWork.Save();
            return;
        }
        #endregion

        #region Helpers
        private async Task<FormInputGroupModel> ToModel(FormInputGroup formInputGroup)
        {
            var formInputGroupModel = new FormInputGroupModel
            {
                FormInputGroupId = formInputGroup.FormInputGroupId,

                // Get form template as model.
                FormTemplate = await ToModel(formInputGroup.FormTemplate),
                FormInputs = formInputGroup.FormInputs.Select(fi => fi.Value)
            };
            return formInputGroupModel;
        }

        private async Task<FormInputGroupModel> GetFormInputGroup(object id)
        {
            // Load form input group from database.
            var formInputGroup = await _unitOfWork.FormInputGroupRepository.Find(id);
            if (formInputGroup == null)
                throw new InvalidOperationException("Form input group not found.");
            return await ToModel(formInputGroup);
        }

        private async Task<SheetModel> ToModel(Sheet sheet)
        {
            // Instantiate sheet model.
            var sheetModel = new SheetModel
            {
                SheetId = sheet.SheetId,
                UserId = sheet.UserId,
            };

            // Instantiate a form input group model for each form input group.
            var formInputGroupModels = new List<FormInputGroupModel>();
            foreach (var formInputGroup in sheet.FormInputGroups)
            {
                // Convert form input group object to model.
                var formInputGroupModel = await ToModel(formInputGroup);
                formInputGroupModels.Add(formInputGroupModel);
            }
            sheetModel.FormGroups = formInputGroupModels.AsEnumerable();

            return sheetModel;
        }

        private async Task<Sheet> ToObject(SheetModel sheetModel)
        {
            var sheet = new Sheet
            {
                UserId = sheetModel.UserId,
                FormInputGroups = new List<FormInputGroup>()
            };

            // Create form input groups.
            foreach (var formInputGroupModel in sheetModel.FormGroups)
            {
                var formTemplate = await _unitOfWork.FormTemplateRepository.Find(formInputGroupModel.FormTemplateId);
                if (formTemplate == null)
                    throw new InvalidOperationException("Form template not found.");

                var formInputGroup = new FormInputGroup
                {
                    FormTemplate = formTemplate,
                    FormInputs = new List<FormInput>()
                };

                for (int i = 0; i < formInputGroupModel.FormInputs.Count(); i++)
                {
                    var formInput = new FormInput
                    {
                        Index = i,
                        Value = formInputGroupModel.FormInputs.ElementAt(i)
                    };
                    formInputGroup.FormInputs.Add(formInput);
                }

                sheet.FormInputGroups.Add(formInputGroup);
            }

            return await Task.FromResult(sheet);
        }
        #endregion
    }
}