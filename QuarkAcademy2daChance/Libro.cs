using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuarkAcademy2daChance
{
    class Libro
    {
        private string _nombre;
        public string Nombre { get => _nombre; set => this._nombre = value; }

        private long _codigoISBN;
        public long CodigoISBN { get => _codigoISBN; set => this._codigoISBN = value; }

        private string _autor;
        public string Autor { get => _autor; set => this._autor = value; }
        private List<Ejemplar> _ejemplares;
        public List<Ejemplar> Ejemplares { get => _ejemplares; }

        public Libro(string nombre, long codigoISBN, string autor)
        {
            Nombre = nombre;
            CodigoISBN = codigoISBN;
            Autor = autor;
            _ejemplares = new List<Ejemplar>();
        }

        public void AgregarEjemplar(Ejemplar ejemplar)
        {
            _ejemplares.Add(ejemplar);
        }

        public bool EjemplarDisponible()
        {
            return _ejemplares.Count != 0;
        }

        public Ejemplar PrestarEjemplar()
        {
            Ejemplar ejemplar = null;
            if (EjemplarDisponible())
            {
                ejemplar = _ejemplares[0];
                _ejemplares.RemoveAt(0);
            }
            return ejemplar;
        }

        public override bool Equals(object obj)
        {
            return obj is Libro libro
                   && _codigoISBN == libro.CodigoISBN;
        }

        public override string ToString()
        {
            return $"Nombre: {_nombre}{(_nombre.Length < 15 ? "\t\t\t" : "\t")}ISBN: {_codigoISBN}\tAutor: {_autor}";
        }
    }
}
