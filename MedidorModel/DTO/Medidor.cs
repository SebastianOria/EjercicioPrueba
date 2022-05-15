using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModel
{
    public class Medidor
    {
        private string id;

        public string Id { get => id; set => id = value; }

        public override string ToString()
        {
            return id;
        }


    }
}
