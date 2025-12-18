using Capa.Shared.Enums;

namespace Capa.Shared.DTOs
{
    public class ListUsersDTO
    {
        public string Id { get; set; } = null!;
        public string Document { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Photo { get; set; }
        public UserType UserType { get; set; }

        public int ProvinceId { get; set; }
        public string Province { get; set; } = null!;
        public string Department { get; set; } = null!;
    }
}
