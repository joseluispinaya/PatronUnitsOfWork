using System.ComponentModel.DataAnnotations;

namespace Capa.Shared.Entities
{
    public class Province
    {
        public int Id { get; set; }

        [Display(Name = "Provincia")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        public int DepartmentId { get; set; }

        public Department? Department { get; set; }
    }
}
