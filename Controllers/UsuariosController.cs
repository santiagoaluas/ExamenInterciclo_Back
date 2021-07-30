using ExamenInterciclo_Back.Dtos;
using ExamenInterciclo_Back.Entities;
using ExamenInterciclo_Back.Helpers;
using ExamenInterciclo_Back.Interfaces;
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
        public readonly IEmail _email;
        public UsuariosController(Datacontext datacontext,
                                   IEmail email)
        {
            _datacontext = datacontext;
            _email = email;

          
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
            usergrabar.email = user.email;
            usergrabar.password = user.password;
            usergrabar.photo = user.foto;
            usergrabar.fecha_nacimiento = user.fechNacimiento;
            usergrabar.habilitado = 0;
            usergrabar.fecha_registro = DateTime.UtcNow;
            _datacontext.Usuario.Add(usergrabar);
            await _datacontext.SaveChangesAsync();
            Respuesta resp = new();
            resp.exito = true;
            resp.mensaje = $"!Usuarios {usergrabar.username} se registro con exito!";
            _email.enviarEmailAuth(usergrabar.email, $"https://protected-woodland-45407.herokuapp.com/Usuarios/VerificarUser/{usergrabar.id}");
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

        [HttpGet("VerificarUser/{id}")]
        public async Task<ActionResult> VerificarUsuario(string id)
        {
            Usuario usu = await _datacontext.Usuario.FirstOrDefaultAsync(x => x.id == id);
            if (usu != null)
            {
                usu.habilitado = 1;
                _datacontext.Usuario.Update(usu);
                await _datacontext.SaveChangesAsync();
                return Ok("Se Habilito su usuario");
            }
            else
            {
                return BadRequest("Error");
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


        [HttpGet("Login/{username}/{password}")]
        public async Task<ActionResult> Login(string username, string password)
        {
            Respuesta resp = new();
            Usuario Usuarios = await _datacontext.Usuario.FirstOrDefaultAsync(x => (x.username == username || x.email == username)  && x.password == password && x.habilitado == 1);
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
