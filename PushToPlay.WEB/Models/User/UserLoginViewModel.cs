using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PushToPlay.WEB.Models
{
    public class UserLoginViewModel : PushToPlayBaseViewModel
    {
        [Display(Name = "Usuário")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Usuário deve ter no mínimo 3 letras")]
        [Required(ErrorMessage = "Informe o usuário")]
        [RegularExpression(@"(\S)+", ErrorMessage = "Informe o usuário")]
        [NotMapped]
        public string UserName { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe a senha")]
        [RegularExpression(@"(\S)+", ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string Password { get; set; }

        [Display(Name = "Lembre-se de mim")]
        [NotMapped]
        public bool RememberMe { get; set; }

        public bool FastLogin { get; set; }
    }
}
