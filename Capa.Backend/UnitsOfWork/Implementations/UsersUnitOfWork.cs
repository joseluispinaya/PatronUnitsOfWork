using Capa.Backend.Repositories.Intefaces;
using Capa.Backend.UnitsOfWork.Intefaces;
using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;
using Microsoft.AspNetCore.Identity;

namespace Capa.Backend.UnitsOfWork.Implementations
{
    public class UsersUnitOfWork : IUsersUnitOfWork
    {
        private readonly IUsersRepository _usersRepository;

        public UsersUnitOfWork(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<User> GetUserAsync(Guid userId) => await _usersRepository.GetUserAsync(userId);

        public async Task<IdentityResult> AddUserAsync(User user, string password) => await _usersRepository.AddUserAsync(user, password);

        public async Task AddUserToRoleAsync(User user, string roleName) => await _usersRepository.AddUserToRoleAsync(user, roleName);

        public async Task CheckRoleAsync(string roleName) => await _usersRepository.CheckRoleAsync(roleName);

        public async Task<User> GetUserAsync(string email) => await _usersRepository.GetUserAsync(email);

        public async Task<bool> IsUserInRoleAsync(User user, string roleName) => await _usersRepository.IsUserInRoleAsync(user, roleName);

        public async Task<SignInResult> LoginAsync(LoginDTO model) => await _usersRepository.LoginAsync(model);

        public async Task LogoutAsync() => await _usersRepository.LogoutAsync();

        public async Task<ActionResponse<IEnumerable<User>>> GetAsync(PaginationDTO pagination) => await _usersRepository.GetAsync(pagination);

        public async Task<ActionResponse<IEnumerable<ListUsersDTO>>> GetListAsync(PaginationDTO pagination) => await _usersRepository.GetListAsync(pagination);

        public async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) => await _usersRepository.GetTotalRecordsAsync(pagination);

        public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword) => await _usersRepository.ChangePasswordAsync(user, currentPassword, newPassword);
    }
}
