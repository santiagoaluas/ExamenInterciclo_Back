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
    public class ProductosController : ControllerBase
    {
        private readonly Datacontext _datacontext;
        public ProductosController(Datacontext datacontext)
        {
            _datacontext = datacontext;
        }

        [HttpGet]
        public async Task<ActionResult> Products_All()
        {
            Respuesta resp = new();
            List<Productos> productosLista =  await _datacontext.Productos.ToListAsync();
            if (productosLista != null)
            {
                resp.exito = true;
                resp.mensaje = "Se obtubo correctamente";
                resp.data = productosLista;
                return Ok(resp);
            }
            else
            {
                resp.exito = false;
                resp.mensaje = "No hubo productos para mostrar";
                return BadRequest(resp);
            }
        }
    }
}
