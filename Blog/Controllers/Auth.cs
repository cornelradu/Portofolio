
using AutoMapper;
using Blog.Dtos;
using Blog.Helpers;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Cryptography;

namespace Blog.Controllers;


[Authorize]
[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
    IUserRepository _userRepository;
    IAuthRepository _authRepository;

    private readonly AuthHelper _authHelper;
    IMapper _mapper;

    public AuthController(IConfiguration config, IUserRepository userRepository, IAuthRepository authRepository)
    {
        _userRepository = userRepository;
        _authRepository = authRepository;
        _authHelper = new AuthHelper(config);
        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserForRegistrationDto, User>();
        }));
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public IActionResult Register(UserForRegistrationDto userForRegistrationDto)
    {
        if (userForRegistrationDto.Password == userForRegistrationDto.PasswordConfirm)
        {
            if (!_userRepository.userExists(userForRegistrationDto.Email))
            {
                byte[] passwordSalt = new byte[128 / 8];
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    rng.GetNonZeroBytes(passwordSalt);
                }

                byte[] passwordHash = _authHelper.GetPasswordHash(userForRegistrationDto.Password, passwordSalt);
                _authRepository.AddEntity<Auth>(new Auth(userForRegistrationDto.Email, passwordHash, passwordSalt));

                if (_authRepository.SaveChanges())
                {
                    User userDb = _mapper.Map<User>(userForRegistrationDto);
                    _userRepository.AddEntity<User>(userDb);
                    if (_userRepository.SaveChanges())
                    {
                        return Ok();
                    }
                }
            }
        }
        throw new Exception("Failed to Register");
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public IActionResult Login(UserForLoginDto userForLogin)
    {
        Auth auth = _authRepository.GetSingleAuth(userForLogin.Email);

        byte[] passwordHash = _authHelper.GetPasswordHash(userForLogin.Password, auth.PasswordSalt);

        for (int index = 0; index < passwordHash.Length; index++)
        {
            if (passwordHash[index] != auth.PasswordHash[index])
            {
                return StatusCode(401, "Incorrect password!");
            }
        }

        User user = _userRepository.GetSingleUserEmail(userForLogin.Email);

        return Ok(new Dictionary<string, string> {
                {"token", _authHelper.CreateToken(user.UserId)}
            });

    }
}
