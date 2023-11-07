using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.DomainDal
{
    public class WalletUser
    {
        public string id_wallet { get; set; }
        public string wallet { get; set; }
        public string privateKey { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set;}
    }
}
