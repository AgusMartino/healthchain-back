using DAL.Managers;
using DAL.Tools.Service;
using DOMAIN.DomainDal;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{

	public sealed class BilleteraService
    {
        #region singleton
        private readonly static BilleteraService _instance = new BilleteraService();

		public static BilleteraService Current
		{
			get
			{
				return _instance;
			}
		}

		private BilleteraService()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

        public void CreateWalletUser(string _id_user)
        {
			try
			{
                var privateKey = "";
                using (var rng = RandomNumberGenerator.Create())
                {
                    var privateKeyBytes = new byte[32];
                    rng.GetBytes(privateKeyBytes);

                    privateKey = "0x" + BitConverter.ToString(privateKeyBytes).Replace("-", "").ToLower();
                }
                WalletUser walletUser = new WalletUser
                {
                    id_wallet = Guid.NewGuid().ToString(),
                    user_id = _id_user,
                    privateKey = privateKey,
                    creation_date = DateTime.Now.ToString(),

                };
				BilleteraManager.Current.CreateWalletUser(walletUser);
            }
			catch (Exception ex)
			{
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }

        public void CreateWalletCompany(string id_company)
        {
            try
            {
                var privateKey = "";
                using (var rng = RandomNumberGenerator.Create())
                {
                    var privateKeyBytes = new byte[32];
                    rng.GetBytes(privateKeyBytes);

                    privateKey = "0x" + BitConverter.ToString(privateKeyBytes).Replace("-", "").ToLower();
                }
                WalletCompany walletComapany = new WalletCompany
                {
                    id_wallet = Guid.NewGuid().ToString(),
                    company_id = id_company,
                    privateKey = privateKey,
                    creation_date = DateTime.Now.ToString(),

                };
                BilleteraManager.Current.CreateWalletCompany(walletComapany);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }
    }

}
