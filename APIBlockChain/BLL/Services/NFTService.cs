﻿using DAL.Managers;
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
                    Estado = nftRequest.Estado,
                    precio = nftRequest.precio
                };
                WalletUser WalletUser = BilleteraManager.Current.GetWalletUser(nftRequest.id_user);
                nft.billetera = WalletUser.wallet;
                WalletCompany WalletCompany = BilleteraManager.Current.GetWalletCompany(nftRequest.id_user_Transfer);
                var TrxTransaccion = NFTManager.Current.TranferNFTWithETHBlockChain(nft, nftRequest.id_user, WalletCompany.wallet, nftRequest.id_user_Transfer).ToString();
                
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

        public NftRequest GetNFTUsuario(string tokenid)
        {
            NftRequest request = new NftRequest();
            try
            {
                request = NFTManager.Current.GetNFTUsuario(tokenid);
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

        public IEnumerable<NftRequest> GetNFTMarketplace(string id_user)
        {
            List<NftRequest> request = new List<NftRequest>();
            try
            {
                string cuit = GetCuitUser(id_user);
                request = (List<NftRequest>)NFTManager.Current.GetNFTMarketplace(cuit);
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
                string url = "https://healthchain-api-usuarios-9e18a4d4a113.herokuapp.com/api/User/GetUser/" + guid;
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
                string url = "https://healthchain-api-usuarios-9e18a4d4a113.herokuapp.com/api/User/ValidateUser/" + usuario;
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

        private string GetCuitUser(string usuario)
        {
            string cuit_empresa = "";
            using (var clientHandler = new HttpClientHandler())
            {
                string url = "https://healthchain-api-usuarios-9e18a4d4a113.herokuapp.com/api/User/GetUser/" + usuario;
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);
                client.DefaultRequestHeaders.Clear();
                var response = client.GetAsync(url).Result;
                var res = response.Content.ReadAsStringAsync().Result;
                dynamic r = JObject.Parse(res);
                cuit_empresa = Convert.ToString(r["cuit_empresa"]);
            }
            return cuit_empresa;
        }

        
    }

}
