using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuarkAcademy2daChance
{
    enum MENU
    {
        SALIR = 1,
        LIBROS = 2,
        SOCIOS = 3
    }
    enum MENU_LIBROS
    {
        LISTAR = 1,
        AGREGAR = 2,
    }
    enum MENU_SOCIOS
    {
        LISTAR = 1,
        AGREGAR = 2,
        CUOTA_MENSUAL = 3
    }

    class Menu
    {
        private List<Libro> libros;
        private List<Ejemplar> ejemplares;
        private List<Prestamo> prestamos;
        private List<Socio> socios;

        private static double cuotaMensualSocioVIP = 1000.0;

        public Menu()
        {
            libros = new List<Libro>();
            libros.Add(new Libro("El principito", 1234567890987, "Nicolás Gomez"));
            libros.Add(new Libro("El fantasma de canterville", 1234567890986, "Marlene Taca"));
            libros.Add(new Libro("Lo que el viento se llevo", 1234567890985, "Blondie Gomez"));
            libros.Add(new Libro("El padrino", 1234567890984, "Selina Gomez"));
            ejemplares = new List<Ejemplar>();
            for (int i = 1; i <= 5; i++)
            {
                Ejemplar ejemplar1 = new Ejemplar(libros[0], i, "Fantasía");
                ejemplares.Add(ejemplar1);
                libros[0].AgregarEjemplar(ejemplar1);
                Ejemplar ejemplar2 = new Ejemplar(libros[1], i, "Ficción");
                ejemplares.Add(ejemplar2);
                libros[1].AgregarEjemplar(ejemplar2);
                Ejemplar ejemplar3 = new Ejemplar(libros[2], i, "Romance");
                ejemplares.Add(ejemplar3);
                libros[2].AgregarEjemplar(ejemplar3);
                Ejemplar ejemplar4 = new Ejemplar(libros[3], i, "Gánsteres");
                ejemplares.Add(ejemplar4);
                libros[3].AgregarEjemplar(ejemplar4);
            }
            socios = new List<Socio>();
            socios.Add(new Socio("Nicolás", "Gomez", 0));
            socios.Add(new Socio("Marlene", "Taca", 1));
            socios.Add(new SocioVIP("Blondie", "Gomez", 2, cuotaMensualSocioVIP));
            socios.Add(new SocioVIP("Selina", "Gomez", 3, cuotaMensualSocioVIP));
            prestamos = new List<Prestamo>();
            Ejemplar ejemplar = libros[0].PrestarEjemplar();
            prestamos.Add(new Prestamo(ejemplar, socios[0]));
            ejemplar = libros[1].PrestarEjemplar();
            prestamos.Add(new Prestamo(ejemplar, socios[2]));
            ejemplar = libros[2].PrestarEjemplar();
            prestamos.Add(new Prestamo(ejemplar, socios[2]));
            ejemplar = libros[3].PrestarEjemplar();
            prestamos.Add(new Prestamo(ejemplar, socios[3]));
        }

        public void MenuPrincipal()
        {
            int opc = 0;
            
            do
            {

                MostrarOpcionesDisponibles(new string[] { "SALIR", "LIBROS", "SOCIOS" }, $"Bienvenido/a. Elija una opción:", out opc, false);

                Console.Clear();
                switch ((MENU) opc)
                {
                    case MENU.LIBROS:
                        MenuLibros();
                        break;
                    case MENU.SOCIOS:
                        MenuSocio();
                        break;
                }
                Console.Clear();
            } while (((MENU) opc) != MENU.SALIR);

            Console.WriteLine("Adios! :). Presione una tecla para salir.");
        }
        private void MenuSocio()
        {
            MostrarOpcionesDisponibles(new string[] { "LISTAR", "AGREGAR", "CUOTA MENSUAL SOCIO VIP" }, "SOCIOS\nElija una opción:", out int opc);
            if (opc == 0) return;

            MENU_SOCIOS opcMenu = (MENU_SOCIOS) opc;

            if (opcMenu == MENU_SOCIOS.LISTAR) MenuListarSocios(socios);
            if (opcMenu == MENU_SOCIOS.AGREGAR) AgregarSocio();
            if (opcMenu == MENU_SOCIOS.CUOTA_MENSUAL) ModificarCuotaMensualSocioVIP();
        }
        private void AgregarSocio()
        {
            Console.Clear();
            Console.WriteLine("QuarkTeca.Agregar un socio.");
            Console.WriteLine("Ingrese el nombre:");
            Utils.ValidarString(5, out string nombre);
            Console.WriteLine("Ingrese el apellido:");
            Utils.ValidarString(5, out string apellido);
            MostrarOpcionesDisponibles(new string[] { "SI", "NO" }, "Es socio VIP?", out int opc);
            if (opc == 0) return;
            bool esSocioVIP = opc == 1;
            //El id lo generaría haciendo: socios[^1].id +1, pero dado que no se van a eliminar Socios utilizo su indice actual en la lista.
            Socio nuevoSocio = (esSocioVIP) ? new SocioVIP(nombre, apellido, socios.Count, cuotaMensualSocioVIP) : new Socio(nombre, apellido, socios.Count);
            socios.Add(nuevoSocio);
        }
        private void MenuListarSocios(IList objetos)
        {
            Socio socio = SeleccionarSocio("SOCIOS\nSeleccione un socio. (En los que figura el valor de cuota mensual es porque son Socios VIP)");

            if (socio == null) return;
        
            int opc;
            do {
                MostrarOpcionesDisponibles(new string[] { "DEVOLVER PRESTAMO", "REALIZAR PRESTAMO", "VER EJEMPLARES PRESTADOS" }, $"SOCIOS\n{socio.Id} {socio.Nombre} {socio.Apellido}\nSeleccione una opción.", out opc);

                if (opc == 1) DevolverPrestamo(socio);
                if (opc == 2) RealizarPrestamo(socio);
                if (opc == 3) Listar(socio.EjemplaresRetirados);

                Console.Clear();
            } while (opc != 0);
        }

        private void DevolverPrestamo(Socio socio)
        {
            if(socio.EjemplaresRetirados.Count == 0)
            {
                Console.WriteLine("El socio no posee ejemplares.");
                Console.ReadLine();
                return;
            }

            Prestamo prestamo;
            if(socio is SocioVIP)
            {
                string[] opcionesDisponibles = new string[socio.EjemplaresRetirados.Count];
                for (int i = 0; i < socio.EjemplaresRetirados.Count; i++)
                {
                    opcionesDisponibles[i] = socio.EjemplaresRetirados[i].Libro.Nombre + " " + socio.EjemplaresRetirados[i].NroEdicion;
                }
                MostrarOpcionesDisponibles(opcionesDisponibles, "Seleccione un ejemplar a devolver:", out int opc);
                if (opc == 0) return;
                int indexEjemplar = opc - 1;
                prestamo = prestamos.First(p => p.Ejemplar.Equals(socio.EjemplaresRetirados[indexEjemplar]));
            }
            else
            {
                Console.WriteLine("Ejemplar a devolver:\n\n" + socio.EjemplaresRetirados[0] + "\n");
                prestamo = prestamos.First(p => p.Ejemplar.Equals(socio.EjemplaresRetirados[0]));
            }
            if (!prestamo.Vigente()) Console.WriteLine("El prestamo no está vigente, supero los 5 días. Se le cobrará un excedente.");
            prestamo.Socio.EliminarEjemplar(prestamo.Ejemplar);
            prestamo.Ejemplar.Libro.AgregarEjemplar(prestamo.Ejemplar);
            prestamos.Remove(prestamo);
            Console.WriteLine("Prestamo devuelto exitosamente.\nPresione una tecla para volver.");
            Console.ReadLine();
        }
        private void RealizarPrestamo(Socio socio)
        {
            if (!socio.PuedeLlevarseEjemplar())
            {
                Console.WriteLine("Este socio alcanzó el máximo de ejemplares que se le puede prestar.");
                Console.ReadLine();
                return;
            }
            Libro libro = SeleccionarLibro("Seleccione que libro quiere prestar.");
            if (libro == null) return;
            if (!libro.EjemplarDisponible())
            {
                Console.WriteLine("El libro seleccionado no posee más ejemplares disponibles.");
                Console.ReadLine();
                return;
            }
            Ejemplar ejemplar = libro.PrestarEjemplar();
            Prestamo prestamo = new Prestamo(ejemplar, socio);
            prestamos.Add(prestamo);
            Console.WriteLine("Se realizó el prestamo exitosamente. Presione una tecla para volver.");
            Console.ReadLine();
        }
        private void ModificarCuotaMensualSocioVIP()
        {
            MostrarOpcionesDisponibles(new string[] { "MODIFICAR"}, "CUOTA MENSUAL SOCIO VIP\nMonto actual:\n" + cuotaMensualSocioVIP + "\nElija una opción:", out int opc);
            if (opc == 0) return;
            Console.WriteLine("Ingrese nuevo valor de la cuota mensual:");
            Utils.ValidarDouble(0, double.MaxValue, out cuotaMensualSocioVIP);
            socios.ForEach(s =>
            {
                if (s is SocioVIP sVIP)
                {
                    sVIP.CuotaMensual = cuotaMensualSocioVIP;
                }
            });
            Console.WriteLine("Valor modificado exitosamente! Presiona una tecla para volver al menu principal.");

        }

        private void MenuLibros()
        {
            MostrarOpcionesDisponibles(new string[] { "LISTAR", "AGREGAR" }, "LIBROS\nElija una opción:", out int opc);
            if (opc == 0) return;

            MENU_LIBROS opcMenu = (MENU_LIBROS)opc;

            if (opcMenu == MENU_LIBROS.LISTAR) MenuListarLibros(libros);
            if (opcMenu == MENU_LIBROS.AGREGAR) AgregarLibro();
        }

        private void AgregarLibro()
        {
            MostrarOpcionesDisponibles(new string[] { "EJEMPLAR", "NUEVO LIBRO" }, "LIBROS\nAGREGAR\nElija una opción:", out int opc);
            if (opc == 0) return;

            Console.Clear();

            if (opc == 1) AgregarEjemplar();
            if (opc == 2) AgregarNuevoLibro();
        }

        private void AgregarNuevoLibro()
        {
            Console.Write("Ingrese el nombre del libro:\n");
            string nombreLibro = Utils.ValidarNombreLibro(libros);
            Console.Write("Ingrese el código ISBN del libro:\n");
            long codigoISBN = Utils.ValidarCodigoISBN(libros);
            Console.Write("Ingrese el nombre del autor del libro:\n");
            Utils.ValidarString(4, out string autor);
            Console.Clear();
            
            Libro nuevoLibro = new Libro(nombreLibro, codigoISBN, autor);

            Console.WriteLine("Datos ingresados:\n\n" + nuevoLibro + "\n");
            MostrarOpcionesDisponibles(new string[] { "AGREGAR" }, "Desea agregarlo?", out int opc);
            if (opc == 0) return;
            libros.Add(nuevoLibro);
            Console.WriteLine("Agregado con éxito!\nPresione una tecla para volver al menu principal.");
            Console.ReadLine();

        }
        private void AgregarEjemplar()
        {
            Libro libro = SeleccionarLibro();
            if (libro == null) return;
            Console.Clear();
            Console.WriteLine("Libro seleccionado:");
            Console.WriteLine(libro);
            Console.WriteLine("Ingrese el número de edición del ejemplar:");
            Utils.ValidarNumeroEdicionEjemplar(libro, out int nroEdicion);
            Console.WriteLine("Ingrese la ubicación donde se encuentra este ejemplar:");
            Utils.ValidarString(3, out string ubicacion);

            Ejemplar nuevoEjemplar = new Ejemplar(libro, nroEdicion, ubicacion);

            Console.Clear();
            Console.WriteLine("Datos ingresados:\n\n" + nuevoEjemplar + "\n");
            MostrarOpcionesDisponibles(new string[] { "AGREGAR"}, "Desea agregarlo?", out int opc);
            if (opc == 0) return;
            libro.AgregarEjemplar(nuevoEjemplar);
            ejemplares.Add(nuevoEjemplar);
            Console.WriteLine("Agregado con éxito!\nPresione una tecla para volver al menu principal.");
            Console.ReadLine();
        }

        private Libro SeleccionarLibro(string mensajePrincipal = null)
        {
            string[] opcionesDisponibles = new string[libros.Count];
            for (int i = 0; i < libros.Count; i++)
            {
                opcionesDisponibles[i] = libros[i].Nombre;
            }

            int opc;

            MostrarOpcionesDisponibles(opcionesDisponibles, mensajePrincipal != null ? mensajePrincipal : "LIBROS\nSeleccione un libro:", out opc);
            if (opc == 0) return null;

            int indiceLibroElegido = opc - 1;

            return libros[indiceLibroElegido];
        }
        private Socio SeleccionarSocio(string mensajePrincipal = null)
        {
            string[] opcionesDisponibles = new string[socios.Count];
            for (int i = 0; i < socios.Count; i++)
            {
                opcionesDisponibles[i] = socios[i].ToString();
            }

            int opc;

            MostrarOpcionesDisponibles(opcionesDisponibles, mensajePrincipal != null ? mensajePrincipal : "SOCIOS\nSeleccione un socio:", out opc);
            if (opc == 0) return null;

            int indiceSocioElegido = opc - 1;

            return socios[indiceSocioElegido];
        }

        private void MenuListarLibros(IList objetos)
        {
            Libro libro = SeleccionarLibro("Seleccione un libro para ver los ejemplares disponibles en la librería.");
            if (libro == null) return;
            Console.WriteLine("Ejemplares:");
            Listar(libro.Ejemplares);
        }

        private void Listar(IList objetos)
        {
            if(objetos.Count == 0)
            {
                Console.WriteLine("\nNo hay datos para mostrar. Presione una tecla para ir al menu principal");
                Console.ReadLine();
                return;
            }

            foreach (var objeto in objetos)
            {
                Console.WriteLine(objeto);
            }
            Console.WriteLine("Presione una tecla para volver.");
            Console.ReadLine();
        }
        private void MostrarOpcionesDisponibles(string[] opciones, string mensajePrincipal, out int opc, bool volverAlMenuPrincipal = true)
        {
            Console.WriteLine("QuarkTeca\n" + mensajePrincipal);
            for (int i = 1; i <= opciones.Length; i++)
            {
                Console.WriteLine($"{i} - {opciones[i - 1]}");
            }
            if (volverAlMenuPrincipal)
            {
                Console.WriteLine("\n0 - Volver menu principal");
            }
            Utils.ValidarEntero(volverAlMenuPrincipal ? 0 : 1, opciones.Length, out opc);
            Console.Clear();
        }
    }
}
