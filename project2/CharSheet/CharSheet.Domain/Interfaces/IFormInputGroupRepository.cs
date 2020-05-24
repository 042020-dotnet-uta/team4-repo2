using System.Collections.Generic;
using System.Threading.Tasks;

namespace CharSheet.Domain.Interfaces
{
    public interface IFormInputGroupRepository: IRepository<FormInputGroup>
    {
        Task<IEnumerable<FormInput>> GetFormInputs(object id);
    }
}