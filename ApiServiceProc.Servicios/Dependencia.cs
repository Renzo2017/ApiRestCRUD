using ApiServiceProc.AccesoDatos.Implementacion;
using ApiServiceProc.AccesoDatos.Interfaces;
using ApiServiceProc.Negocio.Implementacion;
using ApiServiceProc.Negocio.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace ApiServiceProc.Servicios
{
    public static class Dependencia
    {
        public static void InyeccionDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IConexionSQLService, conexionSQLService>(); 
            services.AddScoped<IGeneroService, GeneroService>();
        }
    }
}