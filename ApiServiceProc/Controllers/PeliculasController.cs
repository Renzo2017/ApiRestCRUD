using ApiServiceProc.Entidades;
using ApiServiceProc.Negocio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiServiceProc.Aplicacion.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculasService _peliculasService;

        public PeliculasController(IPeliculasService peliculasService)
        {
            this._peliculasService = peliculasService;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            try
            {
                var peliculasDetalles = await _peliculasService.Obtener();

                if (peliculasDetalles.Count > 0)
                {
                    return Ok(peliculasDetalles);
                }
                else
                {
                    return BadRequest("No se encontraron datos");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Guardar(Peliculas pelicula)
        {
            try
            {
                return Ok(await _peliculasService.Guardar(pelicula));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> Actualizar(Peliculas pelicula)
        {
            try
            {
                var peliculaEncontrada = (await _peliculasService.Obtener()).FirstOrDefault(p => int.Parse(p.GetType().GetProperty("id").GetValue(p, null).ToString()) == pelicula.id);

                if (peliculaEncontrada != null)
                {
                    return Ok(await _peliculasService.Actualizar(pelicula));
                }
                else
                {
                    return BadRequest("No se encontraron datos");
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
                var peliculaEncontrada = (await _peliculasService.Obtener()).FirstOrDefault(p => int.Parse(p.GetType().GetProperty("id").GetValue(p, null).ToString()) == id);

                if (peliculaEncontrada != null)
                {
                    return Ok(await _peliculasService.Borrar(id));
                }
                else
                {
                    return BadRequest("No se encontraron datos");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
