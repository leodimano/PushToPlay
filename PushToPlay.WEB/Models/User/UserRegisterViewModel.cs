using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PushToPlay.WEB.Models
{
    public class UserRegisterViewModel : UserLoginViewModel
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Informe o e-mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Informe um endereço de e-mail válido")]
        [NotMapped]
        public string Email { get; set; }

        [Display(Name = "Confirmar Senha")]
        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage="Senhas devem ser iguais")]
        [NotMapped]
        public string RePassword { get; set; }
    }
}
