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
        Task<TemplateModel> GetTemplate(object id);
        Task<FormTemplateModel> GetFormTemplate(FormTemplate formTemplate);
        Task<FormTemplateModel> GetFormTemplate(object id);
    }

    public partial class BusinessService : IBusinessService
    {
        public async Task<TemplateModel> GetTemplate(object id)
        {
            // Load template from database.
            var template = await _unitOfWork.TemplateRepository.Find(id);
            if (template == null)
                throw new InvalidOperationException("Template not found.");
            var formTemplates = await _unitOfWork.TemplateRepository.GetFormTemplates(id);

            var templateModel = new TemplateModel
            {
                TemplateId = template.TemplateId,
            };

            // Instantiate a form template model for each form template.
            var formTemplateModels = new List<FormTemplateModel>();
            foreach (var formTemplate in formTemplates)
            {
                // Convert form template object to model.
                var formTemplateModel = await GetFormTemplate(formTemplate);
                formTemplateModels.Add(formTemplateModel);
            }
            templateModel.FormTemplates = formTemplateModels.AsEnumerable();
            
            return templateModel;
        }

        public async Task<FormTemplateModel> GetFormTemplate(FormTemplate formTemplate)
        {
            var labels = await _unitOfWork.FormTemplateRepository.GetFormLabels(formTemplate.FormTemplateId);
            return new FormTemplateModel
            {
                FormTemplateId = formTemplate.FormTemplateId,
                Type = formTemplate.Type,

                OffsetTop = formTemplate.FormPosition.OffsetTop,
                OffsetLeft = formTemplate.FormPosition.OffsetLeft,
                XPos = formTemplate.FormPosition.XPos,
                YPos = formTemplate.FormPosition.YPos,

                Labels = labels.Select(formLabel => formLabel.Value)
            };
        }

        public async Task<FormTemplateModel> GetFormTemplate(object id)
        {
            // Load form template from database.
            var formTemplate = await _unitOfWork.FormTemplateRepository.Find(id);
            if (formTemplate == null)
                throw new InvalidOperationException("Form template not found.");
            return await GetFormTemplate(formTemplate);
        }
    }
}