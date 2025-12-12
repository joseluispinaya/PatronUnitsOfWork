using Capa.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Capa.Backend.DTOas
{
    public class UserCreateDTO
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Document { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public int ProvinceId { get; set; }

        [Required]
        public UserType UserType { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; } = null!;

        // FOTO
        public IFormFile? PhotoFile { get; set; }
    }
}
