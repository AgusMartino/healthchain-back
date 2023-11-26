using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Domain
{
    public class BitacoraRequest
    {
        public string id_bitacora {  get; set; }
        public string id_usuario { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string creation_date { get; set; }
    }
}
