using Business.Abstract;
using Business.Messages;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task<IDataResult<Category>> GetById(int categoryId)
        {
            var categoryResult = await _categoryDal.Get(s => s.CategoryId == categoryId);
            if (categoryResult == null)
            {
                return new ErrorDataResult<Category>(BusinessMessages.EntityNotFound);
            }
            return new SuccessDataResult<Category>(categoryResult);
        }
        public async Task<IDataResult<List<Category>>> GetAll()
        {
            var categoryList = await _categoryDal.GetList(null);
            return new SuccessDataResult<List<Category>>(categoryList.ToList());
        }
    }
}
