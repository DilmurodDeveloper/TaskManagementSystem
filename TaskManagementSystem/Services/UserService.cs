using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Repositories;

namespace TaskManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() =>
            await _userRepository.GetAllAsync();

        public async Task<User?> GetUserByIdAsync(int id) =>
            await _userRepository.GetByIdAsync(id);

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            var users = await _userRepository.GetAllAsync();
            return users.FirstOrDefault(u => u.Username == username);
        }

        public async Task<User> AddUserAsync(User user) =>
            await _userRepository.AddAsync(user);

        public async Task UpdateUserAsync(User user) =>
            await _userRepository.UpdateAsync(user);

        public async Task DeleteUserAsync(int id) =>
            await _userRepository.DeleteAsync(id);
    }
}
