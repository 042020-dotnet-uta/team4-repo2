using System.Collections.Generic;
using System.Threading.Tasks;

namespace CharSheet.Domain.Interfaces
{
    public interface ITemplateRepository: IRepository<Template>
    {
        Task<IEnumerable<FormTemplate>> GetFormTemplates(object id);
    }
}