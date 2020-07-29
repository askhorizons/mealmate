﻿using Mealmate.Api.Helpers;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Configuration;
using Mealmate.Core.Entities;
using Mealmate.Core.Paging;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Mealmate.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        //private RoleManager<Role> _roleManager;
        //private UserManager<User> _userManager;
        //private IUserRestaurantService _userRestaurantService;
        //private MealmateSettings _mealmateSettings;
        //private IEmailService _emailService;

        public UserController(
                IUserService userService
                //UserManager<User> userManager,
                //RoleManager<Role> roleManager,
                //IUserRestaurantService userRestaurantService,
                //IOptions<MealmateSettings> options,
                //IEmailService emailService
            )
        {
            _userService = userService;
            //_userManager = userManager;
            //_roleManager = roleManager;
            //_userRestaurantService = userRestaurantService;
            //_mealmateSettings = options.Value;
            //_emailService = emailService;

        }


        #region Read
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            var user = this.User;

            var result = await _userService.Get(user);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserModel>> GetById(int id)
        {
            var result = await _userService.GetById(id);
            if (result == null)
            {
                return NotFound($"User with id {id} no more exists");
            }
            return Ok(result);
        }
        #endregion

        #region create
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] UserModel model)
        {
            await _userService.Create(model);
            //TODO: Add you code here

            if (ModelState.IsValid)
            {
                var user = await _userService.Create(model);
                if (user != null)

                    return Created($"/api/users/{user.Id}", user);
            }

            return BadRequest($"Error registering new user");
        }

        #endregion

        #region Update
        [HttpPost()]
        public async Task<ActionResult> Update([FromBody] UserModel request)
        {
            //TODO: Add you code here
            var result = await _userService.GetById(request.Id);
            if (result == null)
            {

                return NotFound($"User with id {request.Id} no more exists");
            }
            var updatedUser = await _userService.Update(request);

            return Ok(updatedUser);
        }
        #endregion

        #region Delete
        [HttpDelete()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(int userId)
        {
            await _userService.Delete(userId);

            return Ok();
        }
        #endregion
    }
}
