using Capa.Shared.Entities;

namespace Capa.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckDepartmentAsync();
            await CheckCategoriesAsync();
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Fiambres" });
                _context.Categories.Add(new Category { Name = "Gaseosas" });
                _context.Categories.Add(new Category { Name = "Belleza" });
                _context.Categories.Add(new Category { Name = "Calzado" });
                _context.Categories.Add(new Category { Name = "Comida" });
                _context.Categories.Add(new Category { Name = "Cosmeticos" });
                _context.Categories.Add(new Category { Name = "Lacteos" });
                _context.Categories.Add(new Category { Name = "Carnes" });
                _context.Categories.Add(new Category { Name = "Limpieza" });
                _context.Categories.Add(new Category { Name = "Jugetes" });
                _context.Categories.Add(new Category { Name = "Lenceria" });
                _context.Categories.Add(new Category { Name = "Mascotas" });
                _context.Categories.Add(new Category { Name = "Nutrición" });
                _context.Categories.Add(new Category { Name = "Ropa" });
                _context.Categories.Add(new Category { Name = "Tecnología" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckDepartmentAsync()
        {
            if (!_context.Departments.Any())
            {
                _context.Departments.Add(new Department { Name = "Beni" });
                _context.Departments.Add(new Department { Name = "Pando" });
                _context.Departments.Add(new Department { Name = "Oruro" });
                _context.Departments.Add(new Department { Name = "La Paz" });
                _context.Departments.Add(new Department { Name = "Cochabamba" });
                _context.Departments.Add(new Department { Name = "Tarija" });
                _context.Departments.Add(new Department { Name = "Santa Cruz" });
                _context.Departments.Add(new Department { Name = "Chuquisaca" });
                _context.Departments.Add(new Department { Name = "Potosi" });
                await _context.SaveChangesAsync();
            }

        }
    }
}
