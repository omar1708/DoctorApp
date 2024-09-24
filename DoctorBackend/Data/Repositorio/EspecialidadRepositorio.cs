using Data.Interfaces.IRepositorio;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio
{
    internal class MedicoRepositorio : Repositorio<Medico>, IMedicoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public MedicoRepositorio(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }

        public void Actualizar(Medico medico)
        {
            var medicoDb = _db.Medicos.FirstOrDefault(e => e.Id == medico.Id);
            if (medicoDb != null) 
            {
                medicoDb.Appelidos = medico.Appelidos;
                medicoDb.Nombres = medico.Nombres;
                medicoDb.Estado = medico.Estado;
                medicoDb.Telefono = medico.Telefono;
                medicoDb.Genero = medico.Genero;
                medicoDb.Especialidad = medico.Especialidad;
                medicoDb.Direccion = medico?.Direccion;
                medicoDb.FechaActualizacion = DateTime.Now;
                _db.SaveChanges();
            }
        }
    }
}
