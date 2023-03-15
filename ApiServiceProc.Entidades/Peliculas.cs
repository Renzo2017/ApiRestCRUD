using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServiceProc.Entidades
{
    public class Peliculas
    {
        public int id { get; set; }

        public string? Titulo { get; set; }

        public string? Descripcion { get; set;}

        public int Año { get; set; }

        public int IDActor { get; set; }

        public int IDGenero { get; set; }
    }
}
