using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuarkAcademy2daChance
{
    class Prestamo
    {
        private Ejemplar _ejemplar;
        private Socio _socio;
        private DateTime _fechaPrestamo;

        public DateTime FechaPrestamo { get => _fechaPrestamo; set => _fechaPrestamo = value; }
        internal Ejemplar Ejemplar { get => _ejemplar; }
        internal Socio Socio { get => _socio; }

        public Prestamo(Ejemplar ejemplar, Socio socio)
        {
            _ejemplar = ejemplar;
            _socio = socio;
            socio.AgregarEjemplar(ejemplar);
            FechaPrestamo = DateTime.Now;
        }

        public bool Vigente()
        {
            // 7200 minutos = 5 días
            return new TimeSpan(DateTime.Now.Ticks - _fechaPrestamo.Ticks).TotalMinutes <= 7200;
        }
    }
}
