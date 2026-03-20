using FirstApi.Infrastructure.Data;
using FirstApi.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstApi.Application.Dtos;
using FirstApi.Application.Services;

namespace FirstApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [HttpGet]
        public async Task<ActionResult<PagedResultDto<UserResponseDto>>> Get([FromQuery] UserQueryParametersDto parameters)
        {
            var users = await _userService.GetAllAsync(parameters);
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Create(CreateUserDto dto)
        {
            var user = await _userService.CreateAsync(dto);

            var response = new UserResponseDto
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email
            };

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDto>> Update(int id, UpdateUserDto dto)
        {
            var updatedUser = await _userService.UpdateAsync(id, dto);

            if (updatedUser == null)
                return NotFound(new { message = "User not found" });

            return Ok(updatedUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _userService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "User not found" });

            return NoContent();
        }
    }
}