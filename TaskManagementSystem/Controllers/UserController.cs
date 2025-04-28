using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Exceptions;
using TaskManagementSystem.Services;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IMapper mapper, ILogger<UserController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all users...");
            var users = await _userService.GetAllUsersAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            _logger.LogInformation("Retrieved {Count} users.", userDtos.Count());
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Getting user by id: {UserId}", id);
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User not found with id: {UserId}", id);
                throw new CustomException("Foydalanuvchi topilmadi", StatusCodes.Status404NotFound);
            }

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid CreateUserDto model: {@ModelState}", ModelState);
                throw new CustomException("Ma'lumotlar to‘g‘ri emas", StatusCodes.Status400BadRequest);
            }

            _logger.LogInformation("Creating a new user: {@CreateUserDto}", createUserDto);
            var user = _mapper.Map<User>(createUserDto);
            var createdUser = await _userService.AddUserAsync(user);
            var userDto = _mapper.Map<UserDto>(createdUser);
            _logger.LogInformation("User created with id: {UserId}", userDto.Id);
            return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid UpdateUserDto model: {@ModelState}", ModelState);
                throw new CustomException("Ma'lumotlar to‘g‘ri emas", StatusCodes.Status400BadRequest);
            }

            if (id != updateUserDto.Id)
            {
                _logger.LogWarning("Update ID mismatch: RouteId={RouteId}, BodyId={BodyId}", id, updateUserDto.Id);
                throw new CustomException("ID mos emas", StatusCodes.Status400BadRequest);
            }

            _logger.LogInformation("Updating user with id: {UserId}", id);
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User not found for update with id: {UserId}", id);
                throw new CustomException("Foydalanuvchi topilmadi", StatusCodes.Status404NotFound);
            }

            _mapper.Map(updateUserDto, user);
            await _userService.UpdateUserAsync(user);

            var updatedUserDto = _mapper.Map<UserDto>(user);
            _logger.LogInformation("User updated successfully: {UserId}", updatedUserDto.Id);
            return Ok(updatedUserDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting user with id: {UserId}", id);
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User not found for deletion with id: {UserId}", id);
                throw new CustomException("Foydalanuvchi topilmadi", StatusCodes.Status404NotFound);
            }

            await _userService.DeleteUserAsync(id);
            _logger.LogInformation("User deleted successfully: {UserId}", id);
            return NoContent();
        }
    }
}
