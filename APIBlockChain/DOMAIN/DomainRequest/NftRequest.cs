using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.DomainRequest
{
    public class NftRequest
    {
        public string id_user { get; set; }
        public string id_user_Transfer {  get; set; }
        public string TokenNFTid { get; set; }
        public string Nombre_paciente { get; set; }
        public string Apellido_paciente { get; set; }
        public string Dni { get; set; }
        public string Cobertura { get; set; }
        public string Consulta { get; set; }
        public string Patologia { get; set; }
        public string Estado { get; set; }
        public string precio { get; set; }
    }
}
