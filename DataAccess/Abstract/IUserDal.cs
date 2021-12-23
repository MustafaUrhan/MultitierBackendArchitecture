using Core.DataAccess;
using Core.Dtos;
using Core.Entities;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        Task<List<OperationClaimDto>> GetClaims(User user);
    }
    //Task Add(User user);
    //Task<User> GetByEmail(string email);
    //Task<List<OperationClaimDto>> GetClaims(User user);
}
