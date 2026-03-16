using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestión_de_Citas_Médicas
{
    internal class Cita
    {
            public int DoctorID { get; set; }
            public string PacienteDPI { get; set; }
            public DateTime Fecha { get; set; }
            public string Hora { get; set; }
        
    }
}
