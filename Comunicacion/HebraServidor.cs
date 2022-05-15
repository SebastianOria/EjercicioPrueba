using System;
using LecturaModel;
using ServidorSocketUtils;
using LecturaModel.DAL;
using MedidorModel;
using MedidorModel.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Sockets;
using System.Threading;

namespace EjercicioPrueba.Comunicacion
{
   public class HebraServidor
    {
        private ILecturaDAL lecturaDAL = LecturaDALArchivos.GetInstancia();
        public void Ejecutar()
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket serverSocket = new ServerSocket(puerto);
            Console.WriteLine("S: Iniciando servidor en puerto {0}", puerto);
            if (serverSocket.Iniciar())
            {
                while (true)
                {
                    Console.WriteLine("S: Esperando Cliente...");
                    Socket cliente = serverSocket.ObtenerCliente();
                    Console.WriteLine("S: Cliente recibido");

                    //esto estaba en generar comunicacion
                    ClienteCom clienteCom = new ClienteCom(cliente);
                    HebraCliente clienteThread = new HebraCliente(clienteCom);
                    Thread t = new Thread(new ThreadStart(clienteThread.Ejecutar));
                    t.IsBackground = true;
                    t.Start();
                }
            }
            else
            {
                Console.WriteLine("Fail, no se puede levantar server en {0}", puerto);
            }
        }
    }
}

