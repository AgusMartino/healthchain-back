using DOMAIN.DomainDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Adapter
{
	public sealed class WalletCompanyAdapter
	{
        #region singleton
        private readonly static WalletCompanyAdapter _instance = new WalletCompanyAdapter();

		public static WalletCompanyAdapter Current
		{
			get
			{
				return _instance;
			}
		}

		private WalletCompanyAdapter()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

        public WalletCompany adapt(object[] values)
        {
            WalletCompany wallet = new WalletCompany
            {
                id_wallet = values[(int)Columns.address].ToString(),
                wallet = values[(int)Columns.address].ToString(),
                privateKey = values[(int)Columns.privateKey].ToString(),
                company_id = values[(int)Columns.id_empresa].ToString()
            };
            return wallet;
        }
        private enum Columns
        {
            id_billetera,
            address,
            privateKey,
            id_empresa
        }
    }

}
