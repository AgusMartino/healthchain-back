using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.DomainDal
{
    public class Transaccion
    {
        public string id_etherscan { get; set; }
        public string TokenIdNFT { get; set; }
        public string usuario { get; set; }
        public string billetera_origen { get; set; }
        public string billetera_destino { get; set; }
        public string fecha_transaccion { get; set; }
    }
}
