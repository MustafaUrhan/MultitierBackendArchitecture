using Core.Dtos;
using Core.Entities;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);

        Task<List<OperationClaimDto>> GetClaims(User user);
    }
}
