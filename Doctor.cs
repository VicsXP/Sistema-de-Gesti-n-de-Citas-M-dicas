using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestión_de_Citas_Médicas
{
    internal class Doctor
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Especialidad { get; set; }

        public Doctor(int id, string nombre, string especialidad)
        {
            ID = id;
            Nombre = nombre;
            Especialidad = especialidad;
        }
    }
}
