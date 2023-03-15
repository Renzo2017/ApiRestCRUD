using ApiServiceProc.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

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
