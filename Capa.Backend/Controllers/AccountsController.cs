using Capa.Backend.DTOas;
using Capa.Backend.Helpers;
using Capa.Backend.UnitsOfWork.Intefaces;
using Capa.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Capa.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly IImageHelper _imageHelper;

        public AccountsController(IUsersUnitOfWork usersUnitOfWork, IImageHelper imageHelper)
        {
            _usersUnitOfWork = usersUnitOfWork;
            _imageHelper = imageHelper;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromForm] UserCreateDTO model)
        {

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
                message = "Usuario creado correctamente",
                user
            });
        }

    }
}
