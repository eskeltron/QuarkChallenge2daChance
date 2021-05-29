using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuarkAcademy2daChance
{
    class Socio
    {
        public static int EJEMPLARES_CANTIDAD_MAXIMA = 1;

        private string _nombre;
        private string _apellido;
        private int _id;
        private List<Ejemplar> _ejemplaresRetirados;

        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellido { get => _apellido; set => _apellido = value; }
        public int Id { get => _id; set => _id = value; }
        public List<Ejemplar> EjemplaresRetirados { get => _ejemplaresRetirados; }

        public Socio(string nombre, string apellido, int id)
        {
            Nombre = nombre;
            Apellido = apellido;
            Id = id;
            _ejemplaresRetirados = new List<Ejemplar>();
        }

        public bool PuedeAgregarEjemplar()
        {
            int cantidadMaximaEjemplares = (this is SocioVIP) ? SocioVIP.EJEMPLARES_CANTIDAD_MAXIMA : EJEMPLARES_CANTIDAD_MAXIMA;
            return _ejemplaresRetirados.Count < cantidadMaximaEjemplares;
        }

        public void AgregarEjemplar(Ejemplar ejemplar)
        {
            if (!PuedeAgregarEjemplar()) throw new Exception("Este socio no puede agregar más ejemplares.");
                
            _ejemplaresRetirados.Add(ejemplar);
        }

        public Ejemplar EliminarEjemplar(Ejemplar ejemplar)
        {
            Ejemplar ejemplarPrestado = null;
            int index = _ejemplaresRetirados.FindIndex(e => e.Equals(ejemplar));
            if (index > -1)
            {
                ejemplarPrestado = _ejemplaresRetirados[index];
                _ejemplaresRetirados.RemoveAt(index);
            }
            return ejemplarPrestado;
        }

        public override string ToString()
        {
            return $"Id:{_id}\tNombre:{_nombre}\tApellido:{_apellido}";
        }
    }
}
