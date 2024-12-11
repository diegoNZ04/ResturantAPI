using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantAPI.Enuns;

namespace RestaurantAPI.Entities
{
    public class User
    {   // Id
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = default!;
        // Name
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Números e caracteres especiais não são permitidos no nome.")]
        public string Name { get; set; } = default!;
        // Email
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "O Email é obrigatório.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage =
            "O Email deve ter no mínimo 5 e no máximo 100 caracteres.")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        public string Email { get; set; } = default!;
        // Password
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage =
            "A Senha deve ter no mínimo 8 caracteres.")]
        public string PasswordHash { get; set; } = default!;
        // Role
        [Required(ErrorMessage = "O tipo do usuário é obrigatório.")]
        public Role Role { get; set; } = default!;
    }
}