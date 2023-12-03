using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerReparación
{
    public class Cliente
    {
        public string estado { get; set; }
        public double tiempo_llegada { get; set; }
        public double tiempo_espera_acum { get; set; }
        public int num_reparador_atendio { get; set; }
    }
}
