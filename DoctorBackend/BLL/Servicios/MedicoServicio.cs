﻿using AutoMapper;
using BLL.Servicios.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.DTOs;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servicios
{
    public class MedicoServicio : IMedicoServicio
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;

        public MedicoServicio(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
        }

        public async Task Actualizar(MedicoDto modeloDto)
        {
            try
            {
                var medicodDb = await _unidadTrabajo.Medico.ObtenerPrimero(e => e.Id == modeloDto.Id);
                if (medicodDb == null)
                {
                    throw new TaskCanceledException("El medico no existe");
                }

                medicodDb.Apellidos = modeloDto.Apellidos;
                medicodDb.Nombres = modeloDto.Nombres;
                medicodDb.Telefono = modeloDto.Telefono;
                medicodDb.Direccion = modeloDto.Direccion;
                medicodDb.Estado = modeloDto.Estado == 1;
                medicodDb.Genero = modeloDto.Genero;
                medicodDb.EspecialidadId = modeloDto.EspecialidadId;

                _unidadTrabajo.Medico.Actualizar(medicodDb);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception) 
            { 
            }
        }

        public async Task<MedicoDto> Agregar(MedicoDto modeloDto)
        {
            try
            {
                Medico medico = new Medico
                {
                    Apellidos = modeloDto.Apellidos,
                    Nombres = modeloDto.Nombres,
                    Telefono = modeloDto.Telefono,
                    Genero = modeloDto.Genero,
                    Direccion = modeloDto.Direccion,
                    EspecialidadId = modeloDto.EspecialidadId,
                    Estado = modeloDto.Estado == 1,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                };

                await _unidadTrabajo.Medico.Agregar(medico);
                await _unidadTrabajo.Guardar();

                if(medico.Id == 0)
                    throw new TaskCanceledException("El medico no se pudo crear");

                return _mapper.Map<MedicoDto>(medico);
            }
            catch (Exception) 
            {
                throw;
            }
        }

        public async Task<IEnumerable<MedicoDto>> ObtenerTodos()
        {
            try
            {
                var lista = await _unidadTrabajo.Medico.ObtenerTodos(incluirPropiedades: "Especialidad", orderBy: e => e.OrderBy(e => e.Apellidos));
                return _mapper.Map<IEnumerable<MedicoDto>>(lista);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Remover(int id)
        {
            try
            {
                var medicoDb = await _unidadTrabajo.Medico.ObtenerPrimero(e => e.Id == id);
                if (medicoDb == null)
                {
                    throw new TaskCanceledException("El medico no existe");
                }

                _unidadTrabajo.Medico.Remover(medicoDb);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
