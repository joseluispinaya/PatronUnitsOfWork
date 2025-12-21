using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.Shared.DTOs
{
    public class ListProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public float Stock { get; set; }
        public string Category { get; set; } = null!;

        public int CategoryId { get; set; }

        public string? Photo { get; set; }

        public bool IsActive { get; set; }

    }
}
