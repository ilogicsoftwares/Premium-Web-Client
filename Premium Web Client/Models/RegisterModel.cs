using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Premium_Web_Client.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "El Usuario es Requerido")]
        [StringLength(20, ErrorMessage = "Debe contener entre 4 y 20 caracteres", MinimumLength = 4)]
        [DataType(DataType.Text)] 
        [Remote("Validate","Home")]
        public string LoginName { get; set; }
        [Required(ErrorMessage = "El email es Requerido")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Introduzca una dirección de Email Valida")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Repetir el email es Requerido")]
        [DataType(DataType.EmailAddress,ErrorMessage = "Introduca una dirección de Email Valida")]
        [System.ComponentModel.DataAnnotations.Compare("Email")]
        public string ConfirmEmail { get; set; }
        [Required(ErrorMessage = "Contraseña Requerida")]
        [StringLength(8, ErrorMessage = "Debe Contener 8 caracteres", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Repetir Contraseña es Requerido")]
        [StringLength(8, ErrorMessage = "Debe Contener 8 caracteres", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }
        public DateTime DateSingned { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string Token { get; set; }

    }
}