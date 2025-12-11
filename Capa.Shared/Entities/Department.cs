using System.ComponentModel.DataAnnotations;

namespace Capa.Shared.Entities
{
    public class Department
    {
        public int Id { get; set; }

        [Display(Name = "Departamento")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;
        public ICollection<Province>? Provinces { get; set; }
        public int ProvincesNumber => Provinces == null ? 0 : Provinces.Count;
    }
}
