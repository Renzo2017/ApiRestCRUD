using ApiServiceProc.Entidades;

namespace ApiServiceProc.Negocio.Interfaces
{
    public interface IActorService
    {
        public Task<List<Actores>> Obtener();

        public Task<List<Actores>> Guardar(Actores actor);

        public Task<List<Actores>> Actualizar(Actores actor);

        public Task<List<Actores>> Borrar(int id);
    }
}
