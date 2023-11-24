using DOMAIN.DomainDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Adapter
{
	public sealed class WalletAdminAdapter
	{
        #region singleton
        private readonly static WalletAdminAdapter _instance = new WalletAdminAdapter();

		public static WalletAdminAdapter Current
		{
			get
			{
				return _instance;
			}
		}

		private WalletAdminAdapter()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

		public WalletAdmin adapt(object[] values)
		{
			WalletAdmin walletAdmin = new WalletAdmin
			{
				wallet = values[(int)Columns.address].ToString(),
				privatekey = values[(int)Columns.privateKey].ToString()
            };
			return walletAdmin;
		}
		private enum Columns
		{
            address,
            privateKey
        }
    }

}
