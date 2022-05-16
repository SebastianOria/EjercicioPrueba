using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModel.DAL
{
    public class MedidorDALArchivos : IMedidorDAL
    {
        private MedidorDALArchivos(){}
        private static MedidorDALArchivos instancia;
        public static IMedidorDAL GetInstancia()
        {
            if (instancia == null) {

                instancia = new MedidorDALArchivos();

            
            }
            return instancia;

        }

        private static string url = Directory.GetCurrentDirectory();
        private static string archivo = url + "/Medidores.txt";

        public List<Medidor>ObtenerMedidores()
        {
            List<Medidor> lista = new List<Medidor>();
            try
            {
                using (StreamReader reader = new StreamReader(archivo))
                {
                    string texto = "";
                    do
                    {
                        texto = reader.ReadLine();
                        if (texto != null)
                        {
                            string[] arr = texto.Trim().Split(';');
                            Medidor medidor = new Medidor()
                            {
                                Id = arr[0]
                               
                            };
                            lista.Add(medidor );
                        }
                    } while (texto != null);
                }
            }
            catch (Exception ex)
            {
                lista = null;
            }
            return lista;
        }
    


}
}
