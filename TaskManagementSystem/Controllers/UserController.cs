using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Services;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // Get all users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users); 
            return Ok(userDtos);
        }

        // Get user by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            var userDto = _mapper.Map<UserDto>(user); 
            return Ok(userDto);
        }

        // Create new user
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto); 
            var createdUser = await _userService.AddUserAsync(user);
            var userDto = _mapper.Map<UserDto>(createdUser); 
            return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
        }

        // Update user
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (id != updateUserDto.Id)
                return BadRequest("ID mos emas!");

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            _mapper.Map(updateUserDto, user); 
            await _userService.UpdateUserAsync(user);
            var updatedUserDto = _mapper.Map<UserDto>(user); 
            return Ok(updatedUserDto);
        }

        // Delete user
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
