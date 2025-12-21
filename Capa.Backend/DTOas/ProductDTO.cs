using System.ComponentModel.DataAnnotations;

namespace Capa.Backend.DTOas
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo Nombre debe tener máximo 100 caracteres.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El campo Descripción es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El campo Descripción debe tener máximo 500 caracteres.")]
        public string Description { get; set; } = null!;


        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "El campo Inventario es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public float Stock { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una Categoría.")]
        public int CategoryId { get; set; }

        public bool IsActive { get; set; }

        public IFormFile? PhotoFile { get; set; }
    }
}
