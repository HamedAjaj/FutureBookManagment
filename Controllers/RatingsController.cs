using FutureOFTask.Dtos.Rating;
using FutureOFTask.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FutureOFTask.Domain.Entities.Identity;
using FutureOFTask.Domain.IUnitOfWork;
using AutoMapper;
using FutureOFTask.Domain.Entities;
using System.Security.Claims;
using System.Data;

namespace FutureOFTask.Controllers
{
    [Authorize]
    public class RatingsController :BaseAPIController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RatingsController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost("rate")]
        public async Task<ActionResult<RatingCreateDTO>> AddRating(RatingCreateDTO ratingCreateDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null) { return BadRequest(new { Message = "User not found" }); }

            var map = _mapper.Map<Rating>(ratingCreateDTO);
            map.UserId = user.Id;

            await _unitOfWork.Repository<Rating>().AddAsync(map);
            await _unitOfWork.Complete();

            return Ok(new {Message="Rate added :)"});
        }

    }
}
