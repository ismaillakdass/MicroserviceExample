﻿using Course.Shared.Dtos;
using MicroService.IdentityServer.Dtos;
using MicroService.IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.IdentityServer.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController( UserManager<ApplicationUser> userManager)
        {
            _userManager= userManager;
        }


        [HttpPost]
        public async Task<IActionResult> SingUp(SingupDto singupDto)
        {
            var user = new ApplicationUser
            {
                UserName = singupDto.UserName,
                Email = singupDto.Email,
                City = singupDto.City,
            };
            var result = await _userManager.CreateAsync(user, singupDto.Password);

            if (!result.Succeeded) { 
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(x=>x.Description).ToList(),400));           
            }
            return NoContent();
        }

    }
}
