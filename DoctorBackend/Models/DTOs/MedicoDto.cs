using Models.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class MedicoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Apellidos es requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Apellidos debe ser minimo de 1 y maximo de 60 caracteres")]
        public string Appelidos { get; set; }

        [Required(ErrorMessage = "Nombres es requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Nombres debe ser minimo de 1 y maximo de 60 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Direccion es requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Direccion debe ser minimo de 1 y maximo de 100 caracteres")]
        public string Direccion { get; set; }

        [StringLength(40, MinimumLength = 1, ErrorMessage = "Direccion debe ser minimo de 1 y maximo de 40 caracteres")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Genero es Requerido")]
        public char Genero { get; set; }

        [Required(ErrorMessage = "Especialidad es requerida")]
        public int EspecialidadId { get; set; }

        public string NombreEspecialidad { get; set; }

        public int Estado { get; set; } // 1 0
    }
}
