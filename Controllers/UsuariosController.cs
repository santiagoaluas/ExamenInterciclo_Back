using ExamenInterciclo_Back.Dtos;
using ExamenInterciclo_Back.Entities;
using ExamenInterciclo_Back.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenInterciclo_Back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsuariosController : ControllerBase
    {
        public readonly Datacontext _datacontext;
        public UsuariosController(Datacontext datacontext)
        {
            _datacontext = datacontext;
          
        }

        [HttpGet("Login/{username}/{password}")]
        public async Task<ActionResult> LoginAsync(string username, string password)
        {
            Respuesta resp = new();
            Usuario login = await _datacontext.Usuario.Where(x => x.username == username && x.password == password).FirstOrDefaultAsync();
            if (login != null)
            {
                resp.exito = true;
                resp.mensaje = "Se loggeo perfectamente";
                resp.data = login;
                return Ok(resp);
            }
            else
            {
                resp.exito = false;
                resp.mensaje = "!Usuario o Password incorrectos!";
                resp.data = null;
                return BadRequest(resp);
            }

        }

        [HttpPost]

        public async Task<ActionResult> Grabar_Usuario(UsuarioDto user)
        {
            Usuario usergrabar = new();
            usergrabar.id = Guid.NewGuid().ToString();
            usergrabar.username = user.username;
            usergrabar.nombre = user.username;
            usergrabar.apellido = user.apellido;
            usergrabar.password = user.password;
            usergrabar.photo = user.foto;
            usergrabar.fecha_nacimiento = user.fechNacimiento;
            usergrabar.fecha_registro = DateTime.UtcNow;
            _datacontext.Usuario.Add(usergrabar);
            await _datacontext.SaveChangesAsync();
            Respuesta resp = new();
            resp.exito = true;
            resp.mensaje = $"!Usuarios {usergrabar.username} se registro con exito!";
            return Ok(resp);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar_Usuario(String id)
        {
            Respuesta resp = new();
            Usuario user = await _datacontext.Usuario.FirstOrDefaultAsync(x => x.id == id);
            if (user != null)
            {
                _datacontext.Usuario.Remove(user);
                await _datacontext.SaveChangesAsync();
                resp.exito = true;
                resp.mensaje = $"!El usuario {user.username} se elimino correctamente!";
                return Ok(resp);
            }
            else
            {
                resp.exito = false;
                resp.mensaje = $"!El usuario {user.username} no existe!";
                return Ok(resp);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Obtener_Todos_Usuarios(string username)
        {
            Respuesta resp = new();
            List<Usuario> Usuarios = await _datacontext.Usuario.Where(x => x.username != username).ToListAsync();
            if (Usuarios != null)
            {
                resp.exito = true;
                resp.mensaje = "Se loggeo perfectamente";
                resp.data = Usuarios;
                return Ok(resp);
            }
            else
            {
                resp.exito = false;
                resp.mensaje = "!No hay usuarios para mostrar!";
                resp.data = null;
                return BadRequest(resp);
            }

        }

    }
}
