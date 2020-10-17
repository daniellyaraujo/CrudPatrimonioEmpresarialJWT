using System.ComponentModel.DataAnnotations;

namespace CrudPatrimonioEmpresarialJWT.Models.TokenConfig
{
    public class IdentityUser
    {
        public class RegisterUser
        {
            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
            public string Password { get; set; }

            [Compare(nameof(Password), ErrorMessage ="As Senhas não conferem.")]
            public string ConfirmPassword { get; set; }
        }

        public class LoginUser
        {
            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
            public string Password { get; set; }
        }
    }
}
