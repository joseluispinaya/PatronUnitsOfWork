using Capa.Backend.DTOas;
using Capa.Backend.Helpers;
using Capa.Backend.UnitsOfWork.Intefaces;
using Capa.Shared.DTOs;
using Capa.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Capa.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IImageHelper _imageHelper;

        public AccountsController(IUsersUnitOfWork usersUnitOfWork, IConfiguration configuration, IImageHelper imageHelper)
        {
            _usersUnitOfWork = usersUnitOfWork;
            _configuration = configuration;
            _imageHelper = imageHelper;
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] PaginationDTO pagination)
        {
            var response = await _usersUnitOfWork.GetListAsync(pagination);

            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalRecords")]
        public async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _usersUnitOfWork.GetTotalRecordsAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromForm] UserCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Subir foto
            string photoPath = string.Empty;

            if (model.PhotoFile != null)
            {
                photoPath = await _imageHelper.UploadImageAsync(model.PhotoFile, "ImagesSis");
            }

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                Document = model.Document,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                ProvinceId = model.ProvinceId,
                UserType = model.UserType,
                Photo = photoPath
            };

            var result = await _usersUnitOfWork.AddUserAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors.FirstOrDefault()?.Description);

            await _usersUnitOfWork.AddUserToRoleAsync(user, model.UserType.ToString());

            return Ok(new
            {
                message = "Usuario creado correctamente"
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO model)
        {
            var result = await _usersUnitOfWork.LoginAsync(model);
            if (result.Succeeded)
            {
                var user = await _usersUnitOfWork.GetUserAsync(model.Email);
                return Ok(BuildToken(user));
            }
            //return BadRequest("Email o contraseña incorrectos.");
            //return BadRequest(new { message = "Email o contraseña incorrectos." });

            return BadRequest(new { message = "Email o contraseña incorrectos." });
        }

        private TokenDTO BuildToken(User user)
        {
            var claims = new List<Claim>
            {
                //new(ClaimTypes.Name, user.Email!),
                new("Email", user.Email!),
                //new(ClaimTypes.Role, user.UserType.ToString()),
                new("Role", user.UserType.ToString()),
                new("FirstName", user.FirstName),
                new("LastName", user.LastName),
                new("Photo", user.Photo ?? string.Empty),
                new("ProvinceId", user.ProvinceId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(30);
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

    }
}
