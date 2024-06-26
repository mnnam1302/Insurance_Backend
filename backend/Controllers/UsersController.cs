﻿using AutoMapper;
using backend.DTO.User;
using backend.Models;
using backend.Responses;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // POST api/v1/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO userDTO)
        {
            try
            {
                var response = await _userService.Register(userDTO);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] int page, int pageSize)
        {
            try
            {
                var result = _userService.GetAllPaging(page, pageSize);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SeacrhUsersByEmail([FromQuery] string email, int page, int pageSize)
        {
            try
            {
                var result = _userService.SearchUserByEmail(email, page, pageSize);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByID([FromRoute] int id)
        {
            try
            {
                // Chỗ này dùng phương thức Generic chung
                var user = await _userService.GetUserById(id);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserById([FromRoute] int id, [FromBody] UpdateUserDTO userDTO)
        {
            try
            {
                var user = await _userService.GetUserById(id);

                if (user == null)
                {
                    return NotFound("User is not valid");
                }

                userDTO.UserId = id;

                var response = await _userService.UpdateUserById(userDTO);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Summary user based on month, year
        /// </summary>
        /// <returns></returns>
        [HttpGet("summary")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetSummaryUser()
        {
            try
            {
                var result = await _userService.GetSummaryUser();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }
    }
}
