using DAL.Managers;
using DAL.Tools.Service;
using DOMAIN.DomainDal;
using DOMAIN.DomainRequest;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public sealed class NFTService
    {
        #region singleton
        private readonly static NFTService _instance = new NFTService();

		public static NFTService Current
		{
			get
			{
				return _instance;
			}
		}

		private NFTService()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

        public void AddNFT(NftRequest nftRequest)
        {
            try
            {
                NFT nft = new NFT
                {
                    id_nft = Guid.NewGuid().ToString(),
                    TokenNFTid = nftRequest.TokenNFTid,
                    Nombre_paciente = nftRequest.Nombre_paciente,
                    Apellido_paciente = nftRequest.Apellido_paciente,
                    Dni = nftRequest.Dni,
                    Cobertura = nftRequest.Cobertura,
                    Consulta = nftRequest.Consulta,
                    Patologia = nftRequest.Patologia,
                    Estado = nftRequest.Estado
                };
                WalletCompany walletCompany = BilleteraManager.Current.GetWalletCompany(nftRequest.id_user);
                nft.billetera = walletCompany.wallet;
                var TrxTransaccion = NFTManager.Current.AddNFTBlockchainAsync(nft, nftRequest.id_user).ToString();
                nft.TrxTransaccion = TrxTransaccion;
                nft.state_transaccion = TransaccionManager.Current.EstadoTransaccionAsync(nft.TrxTransaccion).ToString();
                Transaccion transaccion = new Transaccion
                {
                    id_etherscan = nft.TrxTransaccion,
                    TokenIdNFT = nft.TokenNFTid,
                    usuario = GetNombreUser(nftRequest.id_user),
                    billetera_origen = walletCompany.wallet,
                    billetera_destino = null
                };
                if(nft.state_transaccion == "Success")
                {
                    //se registra en la base
                    NFTManager.Current.AddNFTDal(nft);
                    TransaccionManager.Current.AddTransaccionDal(transaccion);
                }
                if(nft.state_transaccion == "Failure")
                {
                    TransaccionManager.Current.AddTransaccionDal(transaccion);
                }
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }

        public void TransferNFT(NftRequest nftRequest)
        {
            try
            {
                NFT nft = new NFT
                {
                    id_nft = Guid.NewGuid().ToString(),
                    TokenNFTid = nftRequest.TokenNFTid,
                    Nombre_paciente = nftRequest.Nombre_paciente,
                    Apellido_paciente = nftRequest.Apellido_paciente,
                    Dni = nftRequest.Dni,
                    Cobertura = nftRequest.Cobertura,
                    Consulta = nftRequest.Consulta,
                    Patologia = nftRequest.Patologia,
                    Estado = nftRequest.Estado
                };
                WalletCompany walletCompany = BilleteraManager.Current.GetWalletCompany(nftRequest.id_user);
                nft.billetera = walletCompany.wallet;
                string id_usuario_medico = GetIDUser(nftRequest.id_user_Transfer);
                WalletUser walletUser = BilleteraManager.Current.GetWalletUser(id_usuario_medico);
                var TrxTransaccion = NFTManager.Current.TransferNFTBlockchain(nft, nftRequest.id_user, walletUser.wallet).ToString();
                nft.TrxTransaccion = TrxTransaccion;
                nft.state_transaccion = TransaccionManager.Current.EstadoTransaccionAsync(nft.TrxTransaccion).ToString();
                Transaccion transaccion = new Transaccion
                {
                    id_etherscan = nft.TrxTransaccion,
                    TokenIdNFT = nft.TokenNFTid,
                    usuario = GetNombreUser(nftRequest.id_user),
                    billetera_origen = walletCompany.wallet,
                    billetera_destino = walletUser.wallet
                };
                if (nft.state_transaccion == "Success")
                {
                    NFTManager.Current.TranferNFT(nft, walletUser.wallet);
                    TransaccionManager.Current.AddTransaccionDal(transaccion);
                }
                if (nft.state_transaccion == "Failure")
                {
                    TransaccionManager.Current.AddTransaccionDal(transaccion);
                }

            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }

        public void TranferNFTWithETH(NftRequest nftRequest)
        {
            try
            {
                NFT nft = new NFT
                {
                    id_nft = Guid.NewGuid().ToString(),
                    TokenNFTid = nftRequest.TokenNFTid,
                    Nombre_paciente = nftRequest.Nombre_paciente,
                    Apellido_paciente = nftRequest.Apellido_paciente,
                    Dni = nftRequest.Dni,
                    Cobertura = nftRequest.Cobertura,
                    Consulta = nftRequest.Consulta,
                    Patologia = nftRequest.Patologia,
                    Estado = nftRequest.Estado
                };
                WalletUser WalletUser = BilleteraManager.Current.GetWalletUser(nftRequest.id_user);
                nft.billetera = WalletUser.wallet;
                WalletCompany WalletCompany = BilleteraManager.Current.GetWalletCompany(nftRequest.id_user_Transfer);
                var TrxTransaccion = NFTManager.Current.TranferNFTWithETHBlockChain(nft, nftRequest.id_user, WalletCompany.wallet).ToString();
                nft.TrxTransaccion = TrxTransaccion;
                nft.state_transaccion = TransaccionManager.Current.EstadoTransaccionAsync(nft.TrxTransaccion).ToString();
                Transaccion transaccion = new Transaccion
                {
                    id_etherscan = nft.TrxTransaccion,
                    TokenIdNFT = nft.TokenNFTid,
                    usuario = GetNombreUser(nftRequest.id_user_Transfer),
                    billetera_origen = WalletUser.wallet,
                    billetera_destino = WalletCompany.wallet
                };
                if (nft.state_transaccion == "Success")
                {
                    NFTManager.Current.TranferNFTWithETH(nft, WalletCompany.wallet);
                    TransaccionManager.Current.AddTransaccionDal(transaccion);
                }
                if (nft.state_transaccion == "Failure")
                {
                    TransaccionManager.Current.AddTransaccionDal(transaccion);
                }
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }

        public NftRequest GetNFT(string tokenid)
        {
            NftRequest request = new NftRequest();
            try
            {
                request = NFTManager.Current.GetNFT(tokenid);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return request;
        }

        public IEnumerable<NftRequest> GetNFTList()
        {
            List<NftRequest> request = new List<NftRequest>();
            try
            {
                request = (List<NftRequest>)NFTManager.Current.GetNFTList();
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return request;
        }

        public IEnumerable<NftRequest> GetNFTMarketplace(string cuit_empresa)
        {
            List<NftRequest> request = new List<NftRequest>();
            try
            {
                request = (List<NftRequest>)NFTManager.Current.GetNFTList();
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return request;
        }

        public IEnumerable<NftRequest> GetNFTUser(string id_user)
        {
            List<NftRequest> request = new List<NftRequest>();
            try
            {
                WalletUser wallet = new WalletUser();
                wallet = BilleteraManager.Current.GetWalletUser(id_user);
                request = (List<NftRequest>)NFTManager.Current.GetNFTWallet(wallet.wallet);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return request;
        }
        public IEnumerable<NftRequest> GetNFTCompany(string id_user)
        {
            List<NftRequest> request = new List<NftRequest>();
            try
            {
                WalletCompany wallet = new WalletCompany();
                wallet = BilleteraManager.Current.GetWalletCompany(id_user);
                request = (List<NftRequest>)NFTManager.Current.GetNFTWallet(wallet.wallet);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return request;
        }

        public void SellNFT(NftRequest nftRequest)
        {
            try
            {
                NFT nft = new NFT
                {
                    TokenNFTid = nftRequest.TokenNFTid,
                    Estado = nftRequest.Estado,
                    precio = nftRequest.precio
                };
                NFTManager.Current.SellNFT(nft);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }

        public void modifyInformacionNFT(NftRequest nftRequest)
        {
            try
            {
                NFT nft = new NFT
                {
                    TokenNFTid = nftRequest.TokenNFTid,
                    Apellido_paciente = nftRequest.Apellido_paciente,
                    Nombre_paciente = nftRequest.Nombre_paciente,
                    Dni = nftRequest.Dni,
                    Cobertura = nftRequest.Cobertura,
                    Consulta = nftRequest.Consulta,
                    Patologia = nftRequest.Patologia
                };
                NFTManager.Current.modifyInformacionNFT(nft);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }

        private string GetNombreUser(string guid)
        {
            string user = "";
            using (var clientHandler = new HttpClientHandler())
            {
                string url = "https://localhost:7151/api/User/GetUser/" + guid;
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);
                client.DefaultRequestHeaders.Clear();
                var response = client.GetAsync(url).Result;
                var res = response.Content.ReadAsStringAsync().Result;
                dynamic r = JObject.Parse(res);
                user = Convert.ToString(r["name"]);
            }
            return user;
        }

        private string GetIDUser(string usuario)
        {
            string id = "";
            using (var clientHandler = new HttpClientHandler())
            {
                string url = "https://localhost:7151/api/User/ValidateUser/" + usuario;
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);
                client.DefaultRequestHeaders.Clear();
                var response = client.GetAsync(url).Result;
                var res = response.Content.ReadAsStringAsync().Result;
                dynamic r = JObject.Parse(res);
                id = Convert.ToString(r["id"]);
            }
            return id;
        }
    }

}
