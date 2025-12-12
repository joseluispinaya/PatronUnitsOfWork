using Capa.Backend.UnitsOfWork.Intefaces;
using Capa.Shared.Entities;
using Capa.Shared.Enums;

namespace Capa.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsersUnitOfWork _usersUnitOfWork;

        public SeedDb(DataContext context, IUsersUnitOfWork usersUnitOfWork)
        {
            _context = context;
            _usersUnitOfWork = usersUnitOfWork;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckDepartmentAsync();
            await CheckCategoriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("7645323", "Jose", "Pinaya", "jose@yopmail.com", "739 997 726", "Calle Luna Calle Sol", UserType.Admin);
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
        {
            var user = await _usersUnitOfWork.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    Province = _context.Provinces.FirstOrDefault(),
                    UserType = userType,
                };

                await _usersUnitOfWork.AddUserAsync(user, "123456");
                await _usersUnitOfWork.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.User.ToString());
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
            if (!_context.Departments.Any() && !_context.Provinces.Any())
            {
                var departments = new List<Department>
                {
                    new() {
                        Name = "Beni",
                        Provinces =
                        [
                            new Province { Name = "Vaca Diez" },
                            new Province { Name = "Cercado" },
                            new Province { Name = "Moxos" },
                            new Province { Name = "Marbán" },
                            new Province { Name = "Yacuma" }
                        ]
                    },
                    new() {
                        Name = "Cochabamba",
                        Provinces =
                        [
                            new Province { Name = "Arani" },
                            new Province { Name = "Ayopaya" }
                        ]
                    },
                    new() {
                        Name = "Santa Cruz",
                        Provinces =
                        [
                            new Province { Name = "Chiquitos" },
                            new Province { Name = "Obispo Santiesteban" }
                        ]
                    },
                    new() {
                        Name = "Pando",
                        Provinces =
                        [
                            new Province { Name = "Abuna" },
                            new Province { Name = "Manuripi" }
                        ]
                    },
                    new() {
                        Name = "Oruro",
                        Provinces =
                        [
                            new Province { Name = "Sabaya" },
                            new Province { Name = "Carangas" }
                        ]
                    },
                    new() {
                        Name = "La Paz",
                        Provinces =
                        [
                            new Province { Name = "Aroma" },
                            new Province { Name = "Caranavi" }
                        ]
                    },
                    new() {
                        Name = "Tarija",
                        Provinces =
                        [
                            new Province { Name = "Aniceto Arce" },
                            new Province { Name = "Gran Chaco" }
                        ]
                    },
                    new() {
                        Name = "Chuquisaca",
                        Provinces =
                        [
                            new Province { Name = "Oropeza" },
                            new Province { Name = "Yamparaez" }
                        ]
                    },
                    new() {
                        Name = "Potosi",
                        Provinces =
                        [
                            new Province { Name = "Sud Lipez" },
                            new Province { Name = "Charcas" }
                        ]
                    }
                };

                _context.Departments.AddRange(departments);
                await _context.SaveChangesAsync();
            }
        }
    }
}
