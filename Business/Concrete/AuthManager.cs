using Business.Abstract;
using Business.Messages;
using Core.Dtos;
using Core.Entities;
using Core.Utilities.Hashing;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(ITokenHelper tokenHelper, IUserService userService)
        {
            _tokenHelper = tokenHelper;
            _userService = userService;
        }

        public async Task<IDataResult<User>> Login(UserForLoginDto userForLogin)
        {
            var user = await _userService.GetByEmail(userForLogin.Email);
            if (user == null)
            {
                return new ErrorDataResult<User>(BusinessMessages.UserNotFound);
            }

            var passwordToCheck = HashingHelper.VerifyPasswordHash(userForLogin.Password, user.PasswordHash, user.PasswordSalt);
            if (!passwordToCheck)
            {
                return new ErrorDataResult<User>(BusinessMessages.PasswordNotValid);
            }
            return new SuccessDataResult<User>(user);

        }

        public async Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto)
        {
            var userToCheck = await _userService.GetByEmail(userForRegisterDto.Email);
            if (userToCheck != null)
            {
                return new ErrorDataResult<User>(BusinessMessages.UserAlreadyRegistered);
            }
            var user = CreateUserFromRegisteredDto(userForRegisterDto);
            await _userService.Add(user);
            return new SuccessDataResult<User>(user);
        }
        private User CreateUserFromRegisteredDto(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            return new User()
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                Status = true,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
        }

        public async Task<IDataResult<AccessToken>> CreateToken(User user)
        {
            var operationClaims = await _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, operationClaims);
            return new SuccessDataResult<AccessToken>(accessToken);
        }
    }
}
