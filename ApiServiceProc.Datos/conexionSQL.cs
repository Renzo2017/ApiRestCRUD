using Microsoft.Extensions.Configuration;

namespace ApiServiceProc.Datos
{
    public class conexionSQL
    {
        private string _sql;

        public conexionSQL()
        {
            //var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            //_sql = builder.GetSection("ConnectionStrings:ConexionSQL").Value;
        }
    }
}