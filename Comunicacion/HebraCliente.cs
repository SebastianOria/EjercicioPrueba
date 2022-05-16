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
        private static IMedidorDAL medidorDAL = MedidorDALArchivos.GetInstancia();
        private ClienteCom clienteCom;

        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }

        public void Ejecutar()
        {
            bool containsSearchResult = false;
            clienteCom.Escribir("Ingrese Medidor: ");
            string medidor = clienteCom.Leer();

            List<Medidor> medidora = null;
            lock (medidorDAL)
            {
                medidora = medidorDAL.ObtenerMedidores();
            }

            while (!containsSearchResult) {

                for (int i = 0; i < medidora.Count; i++) {

                    containsSearchResult = medidora[i].ToString().Equals(medidor);
                        
                    if(containsSearchResult==true)
                    {
                        break;

                    }
                }
                if (containsSearchResult == true)
                {
                    break;

                }
                else {
                    
                    clienteCom.Escribir("El medidor no se encuentra en la lista, por favor ingrese un medidor valido: ");
                    medidor = clienteCom.Leer();
                }
               
            }

            clienteCom.Escribir("Ingrese Fecha: ");
            string fecha = clienteCom.Leer();
            clienteCom.Escribir("Ingrese Consumo: ");
            string consumo = clienteCom.Leer();
            Lectura lectura = new Lectura()
            {
                Medidor = medidor,
                Fecha = (DateTime)Convert.ChangeType(fecha,typeof(DateTime)),
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
