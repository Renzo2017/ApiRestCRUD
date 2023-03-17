using ApiServiceProc.Entidades;

namespace ApiServiceProc.Negocio.Interfaces
{
    public interface IGeneroService
    {
        public Task<List<Genero>> Obtener();

        public Task<List<Genero>> Guardar(Genero genero);

        public Task<List<Genero>> Actualizar(Genero genero);

        public Task<List<Genero>> Borrar(int id);
    }
}
