using Core.DataAccess.Concrete.Entityframework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCategoryDal : EFEntityRepositoryBase<Category, NorthwindContext>,ICategoryDal
    {
    }
}
