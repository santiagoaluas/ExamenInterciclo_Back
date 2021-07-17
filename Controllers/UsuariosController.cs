using ExamenInterciclo_Back.Dtos;
using ExamenInterciclo_Back.Entities;
using ExamenInterciclo_Back.Helpers;
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
