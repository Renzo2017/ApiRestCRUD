using ApiServiceProc.AccesoDatos.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ApiServiceProc.AccesoDatos.Implementacion
{
    public class conexionSQLService : IConexionSQLService
    {
        private readonly IConfiguration _iconfiguration;

        public conexionSQLService(IConfiguration configuration)
        {
            this._iconfiguration = configuration;
        }

        public string ObtenerCadenaSQL()
        {
            return _iconfiguration.GetConnectionString("ConexionSQL");
        }
    }
}