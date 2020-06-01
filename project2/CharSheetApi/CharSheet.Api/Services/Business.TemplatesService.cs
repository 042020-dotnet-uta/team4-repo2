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
        Task<IEnumerable<TemplateModel>> GetTemplates(object id);
        Task<TemplateModel> GetTemplate(object id);
        #endregion

        #region POST
        Task<TemplateModel> CreateTemplate(TemplateModel templateModel, Guid userId);
        #endregion
    }

    public partial class BusinessService : IBusinessService
    {
        #region GET
        public async Task<IEnumerable<TemplateModel>> GetTemplates(object id)
        {
            // Load templates from database, filter by user id.
            var templates = (await _unitOfWork.TemplateRepository.Get(template => template.UserId == (Guid)id)).Select(template => template.TemplateId);

            // Templates as template models.
            var templateModels = new List<TemplateModel>();
            foreach (var templateId in templates)
            {
                var templateModel = await GetTemplate(templateId);
                templateModels.Add(templateModel);
            }

            return templateModels.AsEnumerable();
        }

        public async Task<TemplateModel> GetTemplate(object id)
        {
            // Load template from database.
            var template = await _unitOfWork.TemplateRepository.Find(id);
            if (template == null)
                throw new InvalidOperationException("Template not found.");
            return await ToModel(template);
        }
        #endregion

        #region POST
        public async Task<TemplateModel> CreateTemplate(TemplateModel templateModel, Guid userId)
        {
            await AuthenticateUser(userId);

            var template = await ToObject(templateModel);
            template.UserId = userId;

            await _unitOfWork.TemplateRepository.Insert(template);
            await _unitOfWork.Save();

            _logger.LogInformation($"New Template: {template.TemplateId} by {userId}");
            return await ToModel(template);
        }
        #endregion

        #region Helpers
        private async Task<FormTemplateModel> ToModel(FormTemplate formTemplate)
        {
            return await Task.FromResult(new FormTemplateModel
            {
                FormTemplateId = formTemplate.FormTemplateId,
                Type = formTemplate.Type,
                Title = formTemplate.Title,

                OffsetTop = formTemplate.FormPosition.OffsetTop,
                OffsetLeft = formTemplate.FormPosition.OffsetLeft,
                XPos = formTemplate.FormPosition.XPos,
                YPos = formTemplate.FormPosition.YPos,
                Width = formTemplate.FormPosition.Width,
                Height = formTemplate.FormPosition.Height,

                Labels = formTemplate.FormLabels.Select(formLabel => formLabel.Value)
            });
        }

        private async Task<FormTemplateModel> GetFormTemplate(object id)
        {
            // Load form template from database.
            var formTemplate = await _unitOfWork.FormTemplateRepository.Find(id);
            if (formTemplate == null)
                throw new InvalidOperationException("Form template not found.");
            return await ToModel(formTemplate);
        }
        private async Task<TemplateModel> ToModel(Template template)
        {
            var templateModel = new TemplateModel
            {
                TemplateId = template.TemplateId,
            };

            // Instantiate a form template model for each form template.
            var formTemplateModels = new List<FormTemplateModel>();
            foreach (var formTemplate in template.FormTemplates)
            {
                // Convert form template object to model.
                var formTemplateModel = await ToModel(formTemplate);
                formTemplateModels.Add(formTemplateModel);
            }
            templateModel.FormTemplates = formTemplateModels.AsEnumerable();

            return templateModel;
        }

        private async Task<Template> ToObject(TemplateModel templateModel)
        {
            var template = new Template
            {
                FormTemplates = new List<FormTemplate>()
            };

            // Create form templates.
            foreach (var formTemplateModel in templateModel.FormTemplates)
            {
                var formTemplate = new FormTemplate
                {
                    Type = formTemplateModel.Type,
                    Title = formTemplateModel.Title,

                    FormPosition = new FormPosition
                    {
                        OffsetTop = formTemplateModel.OffsetTop,
                        OffsetLeft = formTemplateModel.OffsetLeft,
                        XPos = formTemplateModel.XPos,
                        YPos = formTemplateModel.YPos,
                        Width = formTemplateModel.Width,
                        Height = formTemplateModel.Height
                    },

                    FormLabels = new List<FormLabel>()
                };

                // Create form labels.
                for (int i = 0; i < formTemplateModel.Labels.Count(); i++)
                {
                    var formLabel = new FormLabel
                    {
                        Index = i,
                        Value = formTemplateModel.Labels.ElementAt(i)
                    };
                    formTemplate.FormLabels.Add(formLabel);
                }

                template.FormTemplates.Add(formTemplate);
            }

            return await Task.FromResult(template);
        }
        #endregion
    }
}