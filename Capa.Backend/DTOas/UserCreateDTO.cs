using Capa.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Capa.Backend.DTOas
{
    public class UserCreateDTO
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debes ingresar un correo válido.")]
        public string Email { get; set; } = null!;

        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; } = null!;

        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Dirección")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Address { get; set; } = null!;

        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string PhoneNumber { get; set; } = null!;

        [Display(Name = "Provincia")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int ProvinceId { get; set; }

        [Required]
        public UserType UserType { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(6, ErrorMessage = "El campo {0} debe tener al menos {1} carácteres.")]
        public string Password { get; set; } = null!;

        // FOTO
        public IFormFile? PhotoFile { get; set; }
    }
}
