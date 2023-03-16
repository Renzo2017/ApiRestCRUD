using ApiServiceProc.AccesoDatos.Implementacion;
using ApiServiceProc.AccesoDatos.Interfaces;
using ApiServiceProc.Negocio.Implementacion;
using ApiServiceProc.Negocio.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApiServiceProc.Servicios
{
    public static class Dependencia
    {
        public static void InyeccionDependencia(this IServiceCollection services)
        {
            services.AddScoped<IConexionSQLService, conexionSQLService>();
            services.AddScoped<IGeneroService, GeneroService>();
            services.AddScoped<IActorService, ActorService>();
        }
    }
}