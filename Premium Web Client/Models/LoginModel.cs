using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Premium_Web_Client.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="*El Usuario es Requerido")]
        public string UserName { get; set; }
        [Required (ErrorMessage ="*La Contraseña es Requerida")]
        public string PassWord { get; set; }


    }
}