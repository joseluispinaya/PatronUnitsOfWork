using System.ComponentModel.DataAnnotations;

namespace Capa.Shared.DTOs
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "La contraseña actual es obligatoria.")]
        [StringLength(20, MinimumLength = 6,
        ErrorMessage = "La contraseña actual debe tener entre 6 y 20 caracteres.")]
        public string CurrentPassword { get; set; } = null!;

        [Required(ErrorMessage = "La nueva contraseña es obligatoria.")]
        [StringLength(20, MinimumLength = 6,
        ErrorMessage = "La nueva contraseña debe tener entre 6 y 20 caracteres.")]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "Debe confirmar la nueva contraseña.")]
        [Compare(nameof(NewPassword),
        ErrorMessage = "La nueva contraseña y la confirmación no coinciden.")]
        public string Confirm { get; set; } = null!;
    }
}
