using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuarkAcademy2daChance
{
    class SocioVIP : Socio
    {
        public static new int EJEMPLARES_CANTIDAD_MAXIMA = 3;

        private double _cuotaMensual;
        public double CuotaMensual { get => _cuotaMensual; set => _cuotaMensual = value; }
        public SocioVIP(string nombre, string apellido, int id, double cuotaMensual) : base(nombre, apellido, id)
        {
            CuotaMensual = cuotaMensual;
        }

        public override string ToString()
        {
            return base.ToString() + $"\tCuota mensual: {_cuotaMensual}";
        }

    }
}
