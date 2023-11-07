using Nethereum.Signer;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tools
{

	public sealed class NetConnection
	{
        #region singleton
        private readonly static NetConnection _instance = new NetConnection();

		public static NetConnection Current
		{
			get
			{
				return _instance;
			}
		}

		private NetConnection()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

        public static string net;
		public static string chainId;
        public Web3 Connection(string privateKey)
		{
            var _chainId = int.Parse(chainId); //Nethereum test chain, chainId
			var _net = net.ToString();
            var account = new Account(privateKey, _chainId);
			var web3 = new Web3(account, _net);
			return web3;
		}

		public Account GetAccount(string privateKey)
		{
            var _chainId = int.Parse(chainId); //Nethereum test chain, chainId
            var account = new Account(privateKey, _chainId);
            return account;
        }
    }

}
