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

namespace EjercicioPrueba.Comunicacion
{
    class HebraCliente
    {
        private static ILecturaDAL lecturaDAL = LecturaDALArchivos.GetInstancia();
        private static IMedidorDAL MedidorDAL = MedidorDALArchivos.GetInstancia();
        private ClienteCom clienteCom;

        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }

        public void Ejecutar()
        {
            clienteCom.Escribir("Ingrese Medidor: ");
            string medidor = clienteCom.Leer();
            clienteCom.Escribir("Ingrese Fecha: ");
            string fecha = clienteCom.Leer();
            clienteCom.Escribir("Ingrese Consumo: ");
            string consumo = clienteCom.Leer();
            Lectura lectura = new Lectura()
            {
                Medidor = medidor,
                Fecha = fecha,
                Consumo = consumo,
            };
            lock (lecturaDAL)
            {
                lecturaDAL.AgregarLectura(lectura);
            }
            clienteCom.Desconectar();
        }
    }


}
