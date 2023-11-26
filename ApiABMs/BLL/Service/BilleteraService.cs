using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
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

        public void AddBilletera(string empresa_id)
        {
            try
            {
                using (var clientHandler = new HttpClientHandler())
                {
                    string url = "https://healthchain-api-blockchain-02c3b0c377c2.herokuapp.com/api/Billetera/CreateWalletCompany/" + empresa_id;
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    HttpClient client = new HttpClient(clientHandler);
                    client.DefaultRequestHeaders.Clear();
                    var respose2 = client.GetAsync(url).Result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
