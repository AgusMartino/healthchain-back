using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DOMAIN
{
    public class Empresa
    {
        public Guid id { get; set; }
        public int cuit { get; set; }
        public string name { get; set; }
        public string direccion { get; set; }
    }
}
