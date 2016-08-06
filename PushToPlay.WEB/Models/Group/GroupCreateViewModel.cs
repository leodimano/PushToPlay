using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PushToPlay.WEB.Models
{
    public class GroupCreateViewModel
    {
        [Display(Name="Nome")]
        [MaxLength(50)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Grupo deve ter no mínimo 3 letras")]
        [Required(ErrorMessage = "Nome do grupo é obrigatório")]
        public string Name { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Descrição do grupo é obrigatória")]
        public string Description { get; set; }

        [Display(Name = "Imagem")]
        public string Image { get; set; }
    }
}