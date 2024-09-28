using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class Antecedente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Historia clinica es requerido")]
        public Guid HistoriaClinicaId { get; set; }

        [ForeignKey("HistoriaClinicaId")]
        public HistoriaClinica HistoriaClinica { get; set; }

        [Required(ErrorMessage = "Observacion es requerido")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "La observacion debe ser minimo 1 y maximo de 300 caracteres")]
        public string Observacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}
