using Business.Abstract;
using Core.Dtos;
using Core.Entities;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task Add(User user)
        {
            await _userDal.Add(user);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _userDal.Get(s => s.Email == email);
        }

        public async Task<List<OperationClaimDto>> GetClaims(User user)
        {
            return await _userDal.GetClaims(user);
        }
    }
}
