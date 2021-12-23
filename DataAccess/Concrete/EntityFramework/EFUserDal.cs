using Core.DataAccess.Concrete.Entityframework;
using Core.Dtos;
using Core.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFUserDal : EFEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        public async Task<List<OperationClaimDto>> GetClaims(User user)
        {
            using (var context = new NorthwindContext())
            {
                return await context.OperationClaims
                    .Join(context.UserOperationClaims, oc => oc.Id, uoc => uoc.OperationClaimId, (oc, uoc) => new
                    {
                        Id = oc.Id,
                        Name = oc.Name,
                        UserId = uoc.UserId,
                    })
                    .Where(x => x.UserId == user.Id)
                    .Select(x => new OperationClaimDto()
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }).ToListAsync();
            }
        }
    }
}
