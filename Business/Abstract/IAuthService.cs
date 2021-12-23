using Core.Dtos;
using Core.Entities;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<User>> Login(UserForLoginDto userForLogin);
        Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto);
        Task<IDataResult<AccessToken>> CreateToken(User user);
    }
}
