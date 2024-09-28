using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Apellidos es Requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Apellidos debe se minimo 1 y maximo 60 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Nombres es Requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Nombres debe se minimo 1 y maximo 60 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Direccion es Requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Direccion debe se minimo 1 y maximo 100 caracteres")]
        public string Direccion { get; set; }

        [StringLength(40, MinimumLength = 1, ErrorMessage = "Telefono debe se minimo 1 y maximo 40 caracteres")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Genero es requerido")]
        public char Genero { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public HistoriaClinica HistoriaClinica { get; set; }

        [Display(Name ="Creado Por")]
        public int? CreadoPorId { get; set; }

        [ForeignKey("CreadoPorId")]
        public UsuarioAplicacion CreadoPor { get; set; }

        [Display(Name = "Actualizado Por")]
        public int? ActualizadoPorId { get; set; }

        [ForeignKey("ActualizadoPorId")]
        public UsuarioAplicacion ActualizadoPor { get; set; }

    }


}
