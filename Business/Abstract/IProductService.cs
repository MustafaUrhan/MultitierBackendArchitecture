using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<IResult> Add(Product product);
        Task<IResult> Update(Product product);
        Task<IResult> Delete(int productId);
        Task<IDataResult<Product>> GetById(int productId);
        Task<IDataResult<List<Product>>> GetByCategoryId(int categoryId);
        Task<IDataResult<List<Product>>> GetAll();
        Task<IResult> TransactionScopeTest(Product product);
    }
}
