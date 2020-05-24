using System.Collections.Generic;
using System.Threading.Tasks;
namespace CharSheet.Domain.Interfaces
{
    public interface IFormTemplateRepository: IRepository<FormTemplate>
    {
        Task<IEnumerable<FormLabel>> GetFormLabels(object id);
    }
}