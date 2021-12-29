using Business.Abstract;
using Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLogin)
        {   
            var loginResult = await _authService.Login(userForLogin);
            if (!loginResult.Success)
            {
                return BadRequest(loginResult);
            }

            var tokenResult = await _authService.CreateToken(loginResult.Data);
            if (!tokenResult.Success)
            {
                return BadRequest(tokenResult);
            }
            return Ok(tokenResult);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegister)
        {
            var registerResult = await _authService.Register(userForRegister);
            if (!registerResult.Success)
            {
                return BadRequest(registerResult);
            }
            var tokenResult = await _authService.CreateToken(registerResult.Data);
            if (!tokenResult.Success)
            {
                return BadRequest(tokenResult);
            }
            return Ok(tokenResult);
        }
    }
}
