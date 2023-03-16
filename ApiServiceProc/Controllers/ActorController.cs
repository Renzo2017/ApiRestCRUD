using ApiServiceProc.Entidades;
using ApiServiceProc.Negocio.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiServiceProc.Aplicacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            this._actorService = actorService;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            try
            {
                var actores = await _actorService.Obtener();

                if (actores.Count > 0)
                {
                    return Ok(actores);
                }
                else
                {
                    return BadRequest("No existen datos en la base de datos");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] Actores actor)
        {
            try
            {
                return Ok(await _actorService.Guardar(actor));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar([FromBody] Actores actor)
        {
            try
            {
                var actorEncontrado = (await _actorService.Obtener()).FirstOrDefault(a => a.id == actor.id);

                if (actorEncontrado != null)
                {
                    return Ok(await _actorService.Actualizar(actor));
                }
                else
                {
                    return BadRequest("No se encontró ningun actor con el id proporcionado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Borrar(int id)
        {
            try
            {
                var actorEncontrado = (await _actorService.Obtener()).FirstOrDefault(a => a.id == id);

                if (actorEncontrado != null) 
                {
                    return Ok(await _actorService.Borrar(id));
                }
                else
                {
                    return BadRequest("No se encontró ningún actor con el id proporcionado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
