using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<Category>> GetById(int categoryId);
        Task<IDataResult<List<Category>>> GetAll();
    }
}
