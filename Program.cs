using LecturaModel.DAL;
using LecturaModel;
using MedidorModel;
using MedidorModel.DAL;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EjercicioPrueba.Comunicacion;
using System.Threading;

namespace EjercicioPrueba
{
     class Program
    {
        private static ILecturaDAL lecturaDAL = LecturaDALArchivos.GetInstancia();
        private static IMedidorDAL medidorDAL = MedidorDALArchivos.GetInstancia();
        static bool Menu() {

            bool continuar = true;
            Console.WriteLine("¿Que quiere hacer?");
            Console.WriteLine("1. Ingresar \n 2. Mostrar \n 0. Salir");
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Ingresar();
                    break;
                case "2":
                    Mostrar();
                    break;
                case "0":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Ingrese de nuevo");
                    break;
            }
            return continuar;


        }


        public static void Main(String[] args) 
        {
        
            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.Start();
          
            while (Menu()) ;
        }

        static void Ingresar()
        {
            bool containsSearchResult = false;
            Console.WriteLine("Ingrese Medidor: ");
            string medidor = Console.ReadLine().Trim();

            List<Medidor> medidora = null;
            lock (medidorDAL)
            {
                medidora = medidorDAL.ObtenerMedidores();
            }
            while (!containsSearchResult)
            {
                for (int i = 0; i < medidora.Count; i++)
                {
                    Console.WriteLine(medidora[i]);
                    if (containsSearchResult = medidora[i].ToString().Equals(medidor))
                    {
                        break;

                    }
                }
                if (containsSearchResult == true)
                {
                    break;

                }
                else
                {   Console.WriteLine("El medidor no se encuentra en la lista, por favor ingrese un medidor valido: ");
                    medidor = Console.ReadLine().Trim();
                }
            }
            Console.WriteLine("Ingrese Fecha :");
            string fecha = Console.ReadLine().Trim();
            Console.WriteLine("Ingrese valor de consumo :");
            string consumo = Console.ReadLine().Trim();
            Lectura lectura = new Lectura()
            {
                Medidor = medidor,
                Fecha = (DateTime)Convert.ChangeType(fecha, typeof(DateTime)),
                Consumo = consumo
            };
           
            lock (lecturaDAL)
            {
                lecturaDAL.AgregarLectura(lectura);
            }

        }

        static void Mostrar()
        {
            List<Lectura> lecturas = null;
            lock (lecturaDAL)
            {
                lecturas = lecturaDAL.ObtenerLecturas();
            }
            foreach (Lectura lectura in lecturas)
            {
                Console.WriteLine(lectura);
            }
        }

    }
}
