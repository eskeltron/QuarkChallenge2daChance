using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuarkAcademy2daChance
{
    class Utils
    {
        public static void ValidarEntero(int min, int max, out int opc)
        {
            while (!int.TryParse(Console.ReadLine(), out opc) || opc < min || opc > max)
            {
                Console.WriteLine($"Número fuera de rango. Mínimo: {min}. Máximo:{max}");
            }
        }
        public static void ValidarNumeroEdicionEjemplar(Libro libro, out int nroEdicion)
        {
            while (!int.TryParse(Console.ReadLine(), out nroEdicion) || libro.Ejemplares.Contains(new Ejemplar(libro, nroEdicion, "")) || nroEdicion <= 0)
            {
                if (nroEdicion <= 0)
                {
                    Console.WriteLine($"Por favor ingrese un número mayor a 0.");
                }
                else
                {
                    Console.WriteLine($"Ya existé un ejemplar con ese número de edición.");
                }
            }
        }
        public static void ValidarString(int largoMinimo, out string texto)
        {
            bool largoErroneo = false;
            do
            {
                if (largoErroneo)
                {
                    Console.WriteLine("Largo mínimo: " + largoMinimo);
                }
                texto = Console.ReadLine();
                largoErroneo = texto.Length < largoMinimo;
            } while (largoErroneo);
        }
        public static string ValidarNombreLibro(List<Libro> libros)
        {
            string nombreLibro;
            bool nombreRepetido = false;
            int largoMinimo = 4;
            do
            {
                if (nombreRepetido)
                {
                    Console.WriteLine("Ya existé un libro con ese nombre.");
                }
                
                nombreLibro = Console.ReadLine();
                nombreRepetido = libros.Any(l => l.Nombre == nombreLibro);
                
                if (nombreLibro.Length < largoMinimo)
                {
                    Console.WriteLine("El libro tiene que tener un mínimo de 4 carácteres como nombre.");
                }
            } while (nombreRepetido || nombreLibro.Length < largoMinimo);
            return nombreLibro;
        }

        public static long ValidarCodigoISBN(List<Libro> libros)
        {
            long codigoISBN;
            while (!long.TryParse(Console.ReadLine(), out codigoISBN) || codigoISBN < 0 || codigoISBN.ToString().Length != 13 || libros.Any(l => l.CodigoISBN == codigoISBN))
            {
                Console.WriteLine($"Por favor ingrese un código ISBN válido. Recuerde: 1) Los códigos ISBN tienen 13 digitos de largo. 2) Los códigos ISBN son únicos.");
            }
            return codigoISBN;
        }
        public static void ValidarDouble(double min, double max, out double num)
        {
            while (!double.TryParse(Console.ReadLine(), out num) || num < min || num > max)
            {
                Console.WriteLine($"Por favor ingrese un número entre los valores {min} y {max}.");
            }
        }
    }
}
