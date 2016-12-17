using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Model.Entities
{
    [DataContract]
    public class Login
    {
        [DataMember]
        [Required(ErrorMessage = "Informe o login")]
        [StringLength(50, ErrorMessage = "Tamanho máximo de 50 posições")]
        [EmailAddress(ErrorMessage = "Informe um email válido")]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Informe a senha")]
        [StringLength(50, ErrorMessage = "Tamanho máximo de 50 posições")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataMember]
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
