using Nethereum.Web3;
using Nethereum.HdWallet;
using Nethereum.RLP;
using Nethereum.Signer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBitcoin;
using DAL.Tools;
using Nethereum.Web3.Accounts;
using DOMAIN.DomainDal;
using System.Data.SqlClient;

namespace DAL.Managers
{
	public sealed class BilleteraManager
	{
        #region singleton
        private readonly static BilleteraManager _instance = new BilleteraManager();

		public static BilleteraManager Current
		{
			get
			{
				return _instance;
			}
		}

		private BilleteraManager()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

		public void CreateWalletUser(WalletUser walletUser)
		{
			try
			{
                Account account = NetConnection.Current.GetAccount(walletUser.privateKey);
                walletUser.wallet = account.Address;
                string statement = "INSERT INTO [dbo].[Billetera] (id_billetera, address, privateKey, fecha_creacion) VALUES (@id_billetera, @address, @privateKey, @fecha_creacion)";
                SqlHelper.ExecuteNonQuery(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@id_billetera", Guid.Parse(walletUser.id_wallet)),
                    new SqlParameter("@address", walletUser.wallet),
                    new SqlParameter("@privateKey", walletUser.privateKey),
                    new SqlParameter("@fecha_creacion", DateTime.Parse(walletUser.creation_date))
                });
                BilleteraUserManager.Current.createRelationshipUserBillletera(walletUser);
            }
			catch (Exception ex)
			{

				throw ex;
			}
        }

        public void CreateWalletCompany(WalletCompany walletCompany)
        {
            try
            {
                Account account = NetConnection.Current.GetAccount(walletCompany.privateKey);
                walletCompany.wallet = account.Address;
                string statement = "INSERT INTO [dbo].[Billetera] (id_billetera, address, privateKey, fecha_creacion) VALUES (@id_billetera, @address, @privateKey, @fecha_creacion)";
                SqlHelper.ExecuteNonQuery(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@id_billetera", Guid.Parse(walletCompany.id_wallet)),
                    new SqlParameter("@address", walletCompany.wallet),
                    new SqlParameter("@privateKey", walletCompany.privateKey),
                    new SqlParameter("@fecha_creacion", DateTime.Parse(walletCompany.creation_date))
                });
                BilleteraCompanyManager.Current.createRelationshipCompanyBillletera(walletCompany);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }

}
