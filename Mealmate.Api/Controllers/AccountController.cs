﻿using AutoMapper;
using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Configuration;
using Mealmate.Core.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mealmate.Api.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MealmateSettings _mealmateSettings;
        private readonly IRestaurantService _restaurantService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AccountController(SignInManager<User> signInManager,
          UserManager<User> userManager,
          IOptions<MealmateSettings> options,
          IMapper mapper,
          IRestaurantService restaurantService)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _mealmateSettings = options.Value;
            _restaurantService = restaurantService;
        }

        #region Login
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> CreateToken([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    // Create the token
                    var claims = new[]
                    {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        };

                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_mealmateSettings.Tokens.Key));
                    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                      _mealmateSettings.Tokens.Issuer,
                      _mealmateSettings.Tokens.Audience,
                      claims,
                      expires: DateTime.Now.AddMinutes(30),
                      signingCredentials: signingCredentials);

                    var results = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    };

                    return Ok(results);
                }
                else
                {
                    return Unauthorized("UserName of Password is incorrect");
                }

            }
                return Unauthorized("UserName of Password is incorrect");
           
        }
        #endregion

        #region Change Password
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.OldPassword, false);
            if (result.Succeeded)
            {
                var change = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
                if (change.Succeeded)
                    return Ok();
            }

            return Unauthorized();
        }
        #endregion

        #region Sign Out
        [HttpPost("logout")]
        public async Task<IActionResult> SignOut(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                return Ok();
            }

            return Unauthorized();
        }
        #endregion

        #region Register
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest model)
        {
            //TODO: Add you code here

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //Todo:Temp fix.. Flag is required where request is from frontend or mobile app...
                    if (!string.IsNullOrEmpty(model.RestaurantName))
                    {
                        await _restaurantService.Create(new RestaurantModel
                        {
                            Name = model.RestaurantName,
                            Description = model.RestaurantDescription
                        });
                    }

                    return Created($"/api/users/{user.Id}", _mapper.Map<UserModel>(user));
                }
                else
                {
                    return BadRequest($"Error registering new user");
                }
            }
            return BadRequest(ModelState);
        }
        #endregion
    }
}