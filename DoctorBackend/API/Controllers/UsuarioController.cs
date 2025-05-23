﻿using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entidades;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class UsuarioController : BaseApiController
    {
        private readonly UserManager<UsuarioAplicacion> _userManager;
        private readonly ITokenServicio _tokenServicio;
        private ApiResponse _response;
        private readonly RoleManager<RolAplicacion> _rolManager;

        public UsuarioController(UserManager<UsuarioAplicacion> userManager, ITokenServicio tokenServicio, RoleManager<RolAplicacion> rolManager)
        {
            _userManager = userManager;
            this._tokenServicio = tokenServicio;
            _response = new();
            _rolManager = rolManager;
        }

        [Authorize(Policy = "AdminRol")]
        [HttpGet]//api/usuario
        public async Task<ActionResult> GetUsuarios()
        {
            var usuarios = await _userManager.Users.Select(u => new UsuarioListaDto()
            {
                Username = u.UserName,
                Apellidos = u.Apellidos,
                Nombres = u.Nombres,
                Email = u.Email,
                Rol = String.Join(",", _userManager.GetRolesAsync(u).Result.ToArray())

            }).ToListAsync();

            _response.Resultado = usuarios;
            _response.IsExitosa = true;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        //[Authorize]
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Usuario>> GetUsuario(int id)
        //{
        //    var usuario = await _db.Usuarios.FindAsync(id);
        //    return Ok(usuario);

        //}

        [Authorize(Policy = "AdminRol")]
        [HttpPost("registro")]
        public async Task<ActionResult<UsuarioDto>> Registro(RegistroDto registroDto)
        {
            if(await UsuarioExiste(registroDto.Username))
            {
                return BadRequest("UserName ya esta registrado.");
            }

            var usuario = new UsuarioAplicacion
            {
                UserName = registroDto.Username.ToLower(),
                Email = registroDto.Email.ToLower(),
                Apellidos = registroDto.Apellidos,
                Nombres = registroDto.Nombres,
            };
            
            var resultado = await _userManager.CreateAsync(usuario, registroDto.Password);
            if (!resultado.Succeeded)
            {
                return BadRequest(resultado.Errors);
            }

            var rolResultado = await _userManager.AddToRoleAsync(usuario, registroDto.Rol);
            if (!rolResultado.Succeeded) return BadRequest("Error al agregar el rol al usuario");

            return new UsuarioDto
            {
                Username = usuario.UserName,
                Token = await _tokenServicio.CrearToken(usuario)
            };
        }

        private async Task<bool> UsuarioExiste(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            if (usuario == null) 
            {
                return Unauthorized("Usuario no valido");  
            }
            
            var resultado = await _userManager.CheckPasswordAsync(usuario, loginDto.Password);
            if (!resultado)
            {
                return Unauthorized("Password no valido");
            };

            return new UsuarioDto
            {
                Username = usuario.UserName,
                Token = await _tokenServicio.CrearToken(usuario)
            };
        }

        [Authorize(Policy = "AdminRol")]
        [HttpGet("ListadoRoles")]
        public IActionResult GetRoles()
        {
            var roles = _rolManager.Roles.Select(r => new { NombreRol = r.Name}).ToList();
            _response.Resultado = roles;
            _response.IsExitosa = true;
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }

    }
}
