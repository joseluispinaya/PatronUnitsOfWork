using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;
using Microsoft.AspNetCore.Identity;

namespace Capa.Backend.UnitsOfWork.Intefaces
{
    public interface IUsersUnitOfWork
    {
        Task<ActionResponse<IEnumerable<User>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<IEnumerable<ListUsersDTO>>> GetListAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

        Task<SignInResult> LoginAsync(LoginDTO model);

        Task LogoutAsync();

        Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);


        Task<User> GetUserAsync(Guid userId);

        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);
    }
}
