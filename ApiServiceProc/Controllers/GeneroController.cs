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
        public async Task<List<Genero>> obtener()
        {
            return await _generoService.Obtener();
        }


        [HttpPost]
        public async Task<List<Genero>> Guardar(Genero genero)
        {
            return await _generoService.Guardar(genero);
        }
    }
}
