using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestión_de_Citas_Médicas
{
    internal class Paciente
    {
        public string DPI { get; set; }
        public string Nombre { get; set; }
        public string NumTelefono { get; set; }

        public Paciente(string dpi, string nombre, string numTelefono)
        {
            DPI = dpi;
            Nombre = nombre;
            NumTelefono = numTelefono;
        }
    }
}
