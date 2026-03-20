using System.ComponentModel.DataAnnotations;

namespace FirstApi.Application.Dtos
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Nome is required")]
        [MinLength(3, ErrorMessage = "Nome must have at least 3 characters")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email format is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha is required")]
        [MinLength(6, ErrorMessage = "Senha must have at least 6 characters")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
    }
}