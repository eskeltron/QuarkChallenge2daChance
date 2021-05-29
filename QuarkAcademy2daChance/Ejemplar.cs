using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuarkAcademy2daChance
{
    class Ejemplar
    {
        private Libro _libro;
        private int _nroEdicion;
        private string _ubicacion;

        public string Ubicacion { get => _ubicacion; set => _ubicacion = value; }
        public int NroEdicion { get => _nroEdicion; set => _nroEdicion = value; }
        public Libro Libro { get => _libro; }

        public Ejemplar(Libro libro, int nroEdicion, string ubicacion)
        {
            _libro = libro;
            NroEdicion = nroEdicion;
            Ubicacion = ubicacion;
        }

        public override bool Equals(object obj)
        {
            return obj is Ejemplar ejemplar
                   && _nroEdicion == ejemplar.NroEdicion
                   && _libro.Equals(ejemplar.Libro);
        }

        public override string ToString()
        {
            return $"Libro: {_libro.Nombre}{(_libro.Nombre.Length < 15 ? "\t\t\t" : "\t")}Número edición: {_nroEdicion}\tUbicacion: {_ubicacion}";
        }
    }
}
