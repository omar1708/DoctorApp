using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class RegistroDto
    {
        [Required(ErrorMessage = "Username es requerido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password es requerido")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "El password debe ser minimo 4 y maximo 10 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Apellidos es requerido")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Nombres es requerido")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Email es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Rol es requerido")]
        public string Rol { get; set; }
    }
}
