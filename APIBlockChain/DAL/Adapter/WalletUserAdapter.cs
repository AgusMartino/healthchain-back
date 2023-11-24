using DOMAIN.DomainDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Adapter
{
	public sealed class WalletUserAdapter
	{
        #region singleton
        private readonly static WalletUserAdapter _instance = new WalletUserAdapter();

		public static WalletUserAdapter Current
		{
			get
			{
				return _instance;
			}
		}

		private WalletUserAdapter()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

        public WalletUser adapt(object[] values)
        {
            WalletUser wallet = new WalletUser
            {
                id_wallet = values[(int)Columns.address].ToString(),
                wallet = values[(int)Columns.address].ToString(),
                privateKey = values[(int)Columns.privateKey].ToString(),
                user_id = values[(int)Columns.id_usuario].ToString()
            };
            return wallet;
        }
        private enum Columns
        {
            id_billetera,
            address,
            privateKey,
            id_usuario
        }
    }

}
