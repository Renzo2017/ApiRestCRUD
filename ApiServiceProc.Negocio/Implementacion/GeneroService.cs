using ApiServiceProc.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiServiceProc.Entidades;
using System.Data;
using System.Data.Common;
using Microsoft.Win32.SafeHandles;
using System.Data.SqlClient;
using ApiServiceProc.AccesoDatos.Interfaces;
using System.Linq;

namespace ApiServiceProc.Negocio.Implementacion
{
    public class GeneroService : IGeneroService
    {
        private readonly IConexionSQLService _conexionSQLService;

        public GeneroService(IConexionSQLService conexionSQLService)
        {
            this._conexionSQLService = conexionSQLService;
        }

        public async Task<List<Genero>> Obtener()
        {
            string consulta = "Select * From Genero";
            List<Genero> lista = new List<Genero>();

            using (var cn = new SqlConnection(_conexionSQLService.ObtenerCadenaSQL()))
            {
                await cn.OpenAsync();
                using var cmd = cn.CreateCommand();
                cmd.CommandText = consulta;

                using var reader = await ((DbCommand)cmd).ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Genero genero = new Genero()
                    {
                        Id = reader.GetInt32(0),
                        Descripcion = reader.GetString(1)
                    };
                    lista.Add(genero);
                }
            }
            return lista;
        }

        public async Task<List<Genero>> Guardar(Genero genero)
        {
            string consultaBuscar = "Insert into Genero (descripcion) values (@descripcion)";

            using (var cn = new SqlConnection(_conexionSQLService.ObtenerCadenaSQL()))
            {
                cn.Open();

                using var comando = cn.CreateCommand();
                comando.CommandText = consultaBuscar;
                var descripcionParametro = comando.CreateParameter();

                descripcionParametro.ParameterName = "@descripcion";
                descripcionParametro.Value = genero.Descripcion;
                comando.Parameters.Add(descripcionParametro);

                comando.ExecuteNonQuery();
            }
            return await Obtener();
        }

        public async Task<List<Genero>> Actualizar(Genero genero)
        {
            string consultaActualizar = "Update Genero Set descripcion =@descripcion where id=@id";

            using (var cn = new SqlConnection(_conexionSQLService.ObtenerCadenaSQL()))
            {
                await cn.OpenAsync();

                using var comando = cn.CreateCommand();
                comando.CommandText = consultaActualizar;

                var descripcionParametro = comando.CreateParameter();
                var idParametro = comando.CreateParameter();

                descripcionParametro.ParameterName = "@descripcion";
                descripcionParametro.Value = genero.Descripcion;

                idParametro.ParameterName = "@id";
                idParametro.Value = genero.Id;

                comando.Parameters.Add(descripcionParametro);
                comando.Parameters.Add(idParametro);

                comando.ExecuteNonQuery();
            }
            return await Obtener();

        }

        public async Task<List<Genero>> Borrar(int id)
        {
            string consultaBorrar = "Delete from Genero Where id = @id";

            using var cn = new SqlConnection(_conexionSQLService.ObtenerCadenaSQL());
            {
                await cn.OpenAsync();

                using var comando = cn.CreateCommand();
                comando.CommandText = consultaBorrar;

                var idParametro = comando.CreateParameter();

                idParametro.ParameterName = "@id";
                idParametro.Value = id;

                comando.Parameters.Add(idParametro);

                await comando.ExecuteNonQueryAsync();
            }
            return await Obtener();
        }
    }
}
