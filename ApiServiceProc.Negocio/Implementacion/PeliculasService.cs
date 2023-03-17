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
    public class PeliculasService : IPeliculasService
    {
        private readonly IConexionSQLService _conexionSQLService;

        public PeliculasService(IConexionSQLService conexionSQLService)
        {
            this._conexionSQLService = conexionSQLService;
        }

        public async Task<List<object>> Obtener()
        {
            string consultaBusqueda = "Select P.id, P.Titulo, P.Descripcion, P.Año, G.Descripcion AS Genero, A.Nombre +' ' +A.Apellido AS Actor From " +
                "Peliculas AS P Inner Join Actores AS A On (A.ID = P.IDActor) Inner Join Genero AS G On (P.IDGenero = G.ID)";

            List<object> peliculasDetalles = new List<object>();

            using var cn = new SqlConnection(_conexionSQLService.ObtenerCadenaSQL());

            await cn.OpenAsync();

            using var comando = new SqlCommand(consultaBusqueda, cn);
            using var reader = await comando.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var peliculasResultado = new
                {
                    id = reader["id"],
                    titulo = reader["Titulo"],
                    descripcion = reader["Descripcion"],
                    año = reader["Año"],
                    actor = reader["Actor"],
                    genero = reader["Genero"]

                };
                peliculasDetalles.Add(peliculasResultado);
            }
            return peliculasDetalles;
        }

        public async Task<List<object>> Guardar(Peliculas pelicula)
        {
            string consultaGuardar = "Insert into Peliculas (Titulo, Descripcion, Año, IDActor, IDGenero) values (@Titulo, @Descripcion, @Año, @IDActor, @IDGenero)";

            using var cn = new SqlConnection(_conexionSQLService.ObtenerCadenaSQL());

            await cn.OpenAsync();

            using var comando = new SqlCommand(consultaGuardar, cn);

            comando.Parameters.AddWithValue("@Titulo", pelicula.Titulo);
            comando.Parameters.AddWithValue("@Descripcion", pelicula.Descripcion);
            comando.Parameters.AddWithValue("@Año", pelicula.Año);
            comando.Parameters.AddWithValue("@IDActor", pelicula.IDActor);
            comando.Parameters.AddWithValue("@IDGenero", pelicula.IDGenero);

            await comando.ExecuteNonQueryAsync();

            return await Obtener();
        }

        public async Task<List<object>> Actualizar(Peliculas pelicula)
        {
            string consultaActualizar = "Update Peliculas set Titulo = @Titulo, Descripcion = @Descripcion, Año = @Año, IDActor = @IDActor, IDGenero = @IDGenero Where ID = @ID";

            using var cn = new SqlConnection(_conexionSQLService.ObtenerCadenaSQL());

            await cn.OpenAsync();

            using var comando = new SqlCommand(consultaActualizar, cn);

            comando.Parameters.AddWithValue("@ID", pelicula.id);
            comando.Parameters.AddWithValue("@Titulo", pelicula.Titulo);
            comando.Parameters.AddWithValue("@Descripcion", pelicula.Descripcion);
            comando.Parameters.AddWithValue("@Año", pelicula.Año);
            comando.Parameters.AddWithValue("@IDActor", pelicula.IDActor);
            comando.Parameters.AddWithValue("@IDGenero", pelicula.IDGenero);

            await comando.ExecuteReaderAsync();

           return await Obtener();
        }

        public async Task<List<object>> Borrar(int id)
        {
            string consultaBorra = "Delete from peliculas Where ID = @ID";

            using var cn = new SqlConnection(_conexionSQLService.ObtenerCadenaSQL());

            await cn.OpenAsync();

            using var comando = new SqlCommand(consultaBorra, cn);
            comando.Parameters.AddWithValue("@ID", id);

            await comando.ExecuteNonQueryAsync();

            return await Obtener();
        }
    }
}
