using ApiServiceProc.AccesoDatos.Interfaces;
using ApiServiceProc.Entidades;
using ApiServiceProc.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServiceProc.Negocio.Implementacion
{

    public class ActorService : IActorService
    {
        private readonly IConexionSQLService _conexionSQLService;

        public ActorService(IConexionSQLService conexionSQLService)
        {
            this._conexionSQLService = conexionSQLService;
        }

        public async Task<List<Actores>> Obtener()
        {
            string consultaBuscar = "Select * From Actores";

            List<Actores> actorLista = new List<Actores>();

            using (var cn = new SqlConnection(_conexionSQLService.ObtenerCadenaSQL()))
            {
                await cn.OpenAsync();

                using var comando = new SqlCommand(consultaBuscar, cn);
                using var reader = await comando.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    Actores actor = new Actores()
                    {
                        id = (int)reader["id"],
                        Nombre = reader["nombre"].ToString(),
                        Apellido = reader["apellido"].ToString()
                    };
                    actorLista.Add(actor);
                }
                return actorLista;
            }
        }

        public async Task<List<Actores>> Guardar(Actores actor)
        {
            string consultaGuardar = "Insert into Actores (nombre, apellido) values (@nombre,@apellido)";

            using (var cn = new SqlConnection(_conexionSQLService.ObtenerCadenaSQL()))
            {
                await cn.OpenAsync();

                using var comando = new SqlCommand(consultaGuardar, cn);

                comando.Parameters.AddWithValue("@nombre", actor.Nombre);
                comando.Parameters.AddWithValue("@apellido", actor.Apellido);

                await comando.ExecuteNonQueryAsync();
            }

            return await Obtener();
        }

        public async Task<List<Actores>> Actualizar(Actores actor)
        {
            string consultaActualizar = "Update Actores set nombre = @nombre, apellido = @apellido where id = @id";

            using (var cn = new SqlConnection(_conexionSQLService.ObtenerCadenaSQL()))
            {
                await cn.OpenAsync();

                using var comando = new SqlCommand(consultaActualizar, cn);
                comando.Parameters.AddWithValue("@nombre", actor.Nombre);
                comando.Parameters.AddWithValue("@apellido", actor.Apellido);
                comando.Parameters.AddWithValue("@id", actor.id);
                await comando.ExecuteNonQueryAsync();
            }

            return await Obtener();
        }

        public async Task<List<Actores>> Borrar(int id)
        {
            string consultaBorrar = "Delete from Actores Where id =@id";

            using (var cn = new SqlConnection(_conexionSQLService.ObtenerCadenaSQL()))
            {
                await cn.OpenAsync();
                using var comando = new SqlCommand(consultaBorrar, cn);
                comando.Parameters.AddWithValue("@id", id);
                await comando.ExecuteNonQueryAsync();
            }

           return await Obtener();
        }
    }
}
