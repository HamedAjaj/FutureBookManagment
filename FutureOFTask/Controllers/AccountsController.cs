using AutoMapper;
using FutureOFTask.Domain.Entities.Identity;
using FutureOFTask.Dtos;
using FutureOFTask.Service.TokenService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FutureOFTask.Controllers
{
    public class AccountsController : BaseAPIController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManger;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;


        #region Constructor
        public AccountsController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManger,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManger = signInManger;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        #endregion

        #region Actions
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
          
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mappedUser = _mapper.Map<RegisterDto,AppUser>(registerDto);
            mappedUser.UserName = registerDto.Email.Split('@')[0];

            if(await _userManager.FindByEmailAsync(registerDto.Email) != null)
                return BadRequest(new {Message="Email Already Exists"});

            var result = await _userManager.CreateAsync(mappedUser, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            await _userManager.AddToRoleAsync(mappedUser, "User");
            return Ok(new { Message = "user Added Successfully :) " });
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return BadRequest(new { Message="User not found" });
            var result = await _signInManger.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized(new { Message = "Email or password Incorrect!" });

            return Ok(new UserDto()
            {
                Name = user.Name,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new UserDto()
            {
                Email = email,
                Name = user.Name
            });
        }


        #endregion
    }

}
