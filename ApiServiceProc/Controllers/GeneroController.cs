using ApiServiceProc.Entidades;
using ApiServiceProc.Negocio.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiServiceProc.Aplicacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroService _generoService;

        public GeneroController(IGeneroService generoService)
        {
            this._generoService = generoService;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            try
            {
                return Ok(await _generoService.Obtener());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(Genero genero)
        {
            try
            {
                var listGenero = await _generoService.Guardar(genero);

                if (listGenero.Count > 0)
                {
                    return Ok(listGenero);
                }
                else 
                {
                    return BadRequest("No se encontraron resultados");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar(Genero genero)
        {
            try
            {
                var generoEncontrado = (await _generoService.Obtener()).FirstOrDefault(g => g.Id == genero.Id);

                if (generoEncontrado != null)
                {
                    return Ok(await _generoService.Actualizar(genero));
                }
                else
                {
                    return BadRequest("No existe ningún genero con este ID");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> Borrar(int id)
        {
            try
            {
                var generoEncontrado = (await _generoService.Obtener()).FirstOrDefault(g => g.Id == id);

                if (generoEncontrado != null)
                {
                    return Ok(await _generoService.Borrar(id));
                }
                else
                {
                    return BadRequest("No existe ningún genero con este ID");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
