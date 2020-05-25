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
    public interface ITemplatesService
    {
        Task<TemplateModel> GetTemplate(object id);
        Task<FormTemplateModel> GetFormTemplate(object id);
    }

    public class TemplatesService : ITemplatesService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<TemplatesService> _logger;

        public TemplatesService(ILogger<TemplatesService> logger, CharSheetContext context)
        {
            this._logger = logger;
            this._unitOfWork = new UnitOfWork(context);
        }

        public async Task<TemplateModel> GetTemplate(object id)
        {
            // Load template from database.
            var template = await _unitOfWork.TemplateRepository.Find(id);
            if (template == null)
                throw new InvalidOperationException("Template not found.");
            var formTemplates = await _unitOfWork.TemplateRepository.GetFormTemplates(id);
        }

        public async Task<FormTemplateModel> GetFormTemplate(object id)
        {
            // Load form template from database.
            var formTemplate = await _unitOfWork.FormTemplateRepository.Find(id);
            if (formTemplate != null)
                throw new InvalidOperationException("Form template not found.");
            var labels = await _unitOfWork.FormTemplateRepository.GetFormLabels(id);
            return new FormTemplateModel
            {
                FormTemplateId = formTemplate.FormTemplateId,
                Type = formTemplate.FormStlye.Type,

                OffsetTop = formTemplate.FormPosition.OffsetTop,
                OffsetLeft = formTemplate.FormPosition.OffsetLeft,
                XPos = formTemplate.FormPosition.XPos,
                YPos = formTemplate.FormPosition.YPos,

                Labels = labels.Select(formLabel => formLabel.Value)
            };
        }
    }
}