using Business.Abstract;
using Business.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [ValidationAspect(typeof(ProductValidator), Priority = 1)]
        public async Task<IResult> Add(Product product)
        {
            await _productDal.Add(product);
            return new SuccessResult(BusinessMessages.ProductAdded);
        }

        public async Task<IResult> Update(Product product)
        {
            await _productDal.Update(product);
            return new SuccessResult(BusinessMessages.ProductUpdated);
        }

        public async Task<IResult> Delete(int productId)
        {
            var result = await GetById(productId);
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }
            await _productDal.Delete(result.Data);
            return new SuccessResult(BusinessMessages.ProductDeleted);
        }

        public async Task<IDataResult<Product>> GetById(int productId)
        {
            var productResult = await _productDal.Get(s => s.ProductId == productId);
            if (productResult == null)
            {
                return new ErrorDataResult<Product>(BusinessMessages.EntityNotFound);
            }
            return new SuccessDataResult<Product>(productResult);
        }

        public async Task<IDataResult<List<Product>>> GetByCategoryId(int categoryId)
        {
            var productList = await _productDal.GetList(s => s.CategoryId == categoryId);
            return new SuccessDataResult<List<Product>>(productList.ToList());
        }

        public async Task<IDataResult<List<Product>>> GetAll()
        {
            var productList = await _productDal.GetList(null);
            return new SuccessDataResult<List<Product>>(productList.ToList());
        }

        [TransactionScopeAspect]
        public async Task<IResult> TransactionScopeTest(Product product)
        {
            await _productDal.Update(product);
            await _productDal.Add(product);
            return new SuccessResult();
        }
    }
}
