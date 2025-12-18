using Capa.Backend.Data;
using Capa.Backend.Helpers;
using Capa.Backend.Repositories.Intefaces;
using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Capa.Shared.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Capa.Backend.Repositories.Implementations
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public UsersRepository(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            var user = await _context.Users
                .Include(u => u.Province!)
                .ThenInclude(s => s.Department)
                .FirstOrDefaultAsync(x => x.Id == userId.ToString());
            return user!;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<User> GetUserAsync(string email)
        {
            var user = await _context.Users
                .Include(u => u.Province!)
                .ThenInclude(c => c.Department)
                .FirstOrDefaultAsync(x => x.Email == email);
            return user!;
        }


        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginDTO model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ActionResponse<IEnumerable<User>>> GetAsync(PaginationDTO pagination)
        {
            IQueryable<User> queryable = _context.Users
                .Include(x => x.Province);

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                string filter = pagination.Filter.Trim();

                queryable = queryable.Where(x =>
                    EF.Functions.Like(x.FirstName, $"%{filter}%") ||
                    EF.Functions.Like(x.LastName, $"%{filter}%"));
            }

            var users = await queryable
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .Paginate(pagination)
                .ToListAsync();

            return new ActionResponse<IEnumerable<User>>
            {
                WasSuccess = true,
                Result = users
            };
        }

        public async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
        {
            IQueryable<User> queryable = _context.Users;

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                string filter = pagination.Filter.Trim();

                queryable = queryable.Where(x =>
                    EF.Functions.Like(x.FirstName, $"%{filter}%") ||
                    EF.Functions.Like(x.LastName, $"%{filter}%"));
            }

            int count = await queryable.CountAsync();

            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = count
            };
        }

        public async Task<ActionResponse<IEnumerable<ListUsersDTO>>> GetListAsync(PaginationDTO pagination)
        {
            var queryable = _context.Users
                .Include(x => x.Province!)
                .ThenInclude(p => p.Department)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                string filter = pagination.Filter.Trim();

                queryable = queryable.Where(x =>
                    EF.Functions.Like(x.FirstName, $"%{filter}%") ||
                    EF.Functions.Like(x.LastName, $"%{filter}%"));
            }

            var users = await queryable
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .Paginate(pagination)
                .Select(x => new ListUsersDTO
                {
                    Id = x.Id,
                    Document = x.Document,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email!,
                    PhoneNumber = x.PhoneNumber!,
                    Photo = x.Photo,
                    UserType = x.UserType,
                    ProvinceId = x.ProvinceId,
                    Province = x.Province!.Name,
                    Department = x.Province!.Department!.Name
                })
                .ToListAsync();

            return new ActionResponse<IEnumerable<ListUsersDTO>>
            {
                WasSuccess = true,
                Result = users
            };
        }

    }
}
