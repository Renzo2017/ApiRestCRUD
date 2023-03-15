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
            //services.AddSingleton(configuration);
            services.AddTransient<IDbConnection>(sp => new SqlConnection(configuration.GetConnectionString("ConexionSQL")));
            services.AddScoped<IGeneroService, GeneroService>();
        }
    }
}