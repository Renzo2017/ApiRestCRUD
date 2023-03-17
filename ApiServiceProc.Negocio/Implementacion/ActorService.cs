#region Bibliotecas
using ApiServiceProc.AccesoDatos.Interfaces;
using ApiServiceProc.Entidades;
using ApiServiceProc.Negocio.Interfaces;
using System.Data.SqlClient;
using System.Drawing;
#endregion

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
            #region Obtiene una lista de todos los registros de la base de datos

            //Consulta de busqueda en la base de datos
            string consultaBuscar = "Select * From Actores";

            //Creo un objeto de tipo lista de actores
            List<Actores> actorLista = new List<Actores>();

            //Creo un objeto de tipo conexion sql y le paso la cadena de conexion obtenida en el servidio
            using var cn = new SqlConnection(_conexionSQLService.ObtenerCadenaSQL());

            //Abro la cadena de conexión de forma asíncrona.
            await cn.OpenAsync();

            //Creo un nuevo objeto sqlCommand y le paso la consulta y la conexión
            using var comando = new SqlCommand(consultaBuscar, cn);

            //creo el datareader del comando de forma asíncrona
            using var reader = await comando.ExecuteReaderAsync();

            //Leo todos los elementos del reader (iterador)
            while (await reader.ReadAsync())
            {
                //Creo un nuevo objeto de tipo actor 
                Actores actor = new Actores()
                {
                    //le asigno el valor de los elmentos del iterador
                    id = (int)reader["ID"],
                    Nombre = reader["Nombre"].ToString(),
                    Apellido = reader["Apellido"].ToString()
                };

                //Agrego el nuevo objeto a la lista de actores
                actorLista.Add(actor);
            }

            //Devuelvo la lista obtenida.
            return actorLista;

            #endregion
        }

        public async Task<List<Actores>> Guardar(Actores actor)
        {
            #region Guarda un nuevo registro en la base de datos

            //Consulta de guardar en la base de datos
            string consultaGuardar = "Insert Into Actores (Nombre, Apellido) values (@Nombre, @Apellido)";

            //Creo un objeto de tipo conexion sql y le paso la cadena de conexion obtenida en el servidio
            using var cn = new SqlConnection(_conexionSQLService.ObtenerCadenaSQL());

            //Abro la cadena de conexión de forma asíncrona.
            await cn.OpenAsync();

            //Creo un nuevo objeto sqlCommand y le paso la consulta y la conexión
            using var comando = new SqlCommand(consultaGuardar, cn);

            //Le asigno los parametros de la consulta al comando con su respectivo valor
            comando.Parameters.AddWithValue("@Nombre", actor.Nombre);
            comando.Parameters.AddWithValue("@Apellido", actor.Apellido);

            //Ejecuto las instrucciones del comando sql de forma asíncrona.
            await comando.ExecuteNonQueryAsync();

            //Devuelvo una lista de todos los registros de forma asíncrona, incluyendo el recien guardado.
            return await Obtener();

            #endregion
        }

        public async Task<List<Actores>> Actualizar(Actores actor)
        {
            #region Actualiza un registro en la base de datos

            //Consulta de actualizar un registro en la base de datos
            string consultaActualizar = "Update Actores Set Nombre = @Nombre, Apellido = @Apellido Where ID = @ID";

            //Creo un objeto de tipo conexion sql y le paso la cadena de conexion obtenida en el servidio
            using var cn = new SqlConnection(_conexionSQLService.ObtenerCadenaSQL());

            //Abro la cadena de conexión de forma asíncrona.
            await cn.OpenAsync();

            //Creo un nuevo objeto sqlCommand y le paso la consulta y la conexión
            using var comando = new SqlCommand(consultaActualizar, cn);

            //Le asigno los parametros de la consulta al comando con su respectivo valor
            comando.Parameters.AddWithValue("@ID", actor.id);
            comando.Parameters.AddWithValue("@Nombre", actor.Nombre);
            comando.Parameters.AddWithValue("@Apellido", actor.Apellido);

            //Ejecuto las instrucciones del comando sql de forma asíncrona.
            await comando.ExecuteNonQueryAsync();

            //Devuelvo una lista de todos los registros de forma asíncrona, incluyendo el recien actualizado.
            return await Obtener();

            #endregion
        }

        public async Task<List<Actores>> Borrar(int id)
        {
            #region Borra un registro en la base de datos

            //Consulta de borrar un registro en la base de datos
            string consultaBorrar = "Delete From Actores Where ID = @ID";

            //Creo un objeto de tipo conexion sql y le paso la cadena de conexion obtenida en el servidio
            using var cn = new SqlConnection(_conexionSQLService.ObtenerCadenaSQL());

            //Abro la cadena de conexión de forma asíncrona.
            await cn.OpenAsync();

            //Creo un nuevo objeto sqlCommand y le paso la consulta y la conexión
            using var comando = new SqlCommand(consultaBorrar, cn);

            //Le asigno el parametro de la consulta al comando con su respectivo valor
            comando.Parameters.AddWithValue("@ID", id);

            //Ejecuto las instrucciones del comando sql de forma asíncrona.
            await comando.ExecuteNonQueryAsync();

            //Devuelvo una lista de todos los registros de forma asíncrona, para visualizar que el registro borrado ya no se muestra.
            return await Obtener();

            #endregion
        }
    }
}
