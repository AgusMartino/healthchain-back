using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.Tools.Service
{
	public sealed class BitacoraService
	{
		#region singleton
		private readonly static BitacoraService _instance = new BitacoraService();

		public static BitacoraService Current
		{
			get
			{
				return _instance;
			}
		}

		private BitacoraService()
		{
			//Implent here the initialization of your singleton
		}
		#endregion

		public void AddBitacora(string type, string description, string id_usuario)
		{
            try
            {
                using (var clientHandler = new HttpClientHandler())
                {
                    string url = "https://healthchain-api-bitacora-8ac3b5dd6f8a.herokuapp.com/api/Bitacora/AddBitacora";
                    string parameter = @"{'id_bitacora': '', 'id_usuario': '" + id_usuario + "', 'name': '', 'lastname': '', 'description': '" + description + "', 'type': '" + type + "', 'creation_date': ''}";
                    dynamic json = JObject.Parse(parameter);
                    var httpContent = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    HttpClient client = new HttpClient(clientHandler);
                    client.DefaultRequestHeaders.Clear();
                    var respose2 = client.PostAsync(url, httpContent).Result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
	}
}