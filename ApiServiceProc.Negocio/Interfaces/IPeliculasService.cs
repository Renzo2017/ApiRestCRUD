using ApiServiceProc.Entidades;

namespace ApiServiceProc.Negocio.Interfaces
{
    public interface IPeliculasService
    {
        public Task<List<object>> Obtener();

        public Task<List<object>> Guardar(Peliculas pelicula);

        public Task<List<object>> Actualizar(Peliculas pelicula);

        public Task<List<object>> Borrar(int id);
    }
}
