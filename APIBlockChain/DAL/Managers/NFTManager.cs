using ADRaffy.ENSNormalize;
using DAL.Adapter;
using DAL.Tools;
using DAL.Tools.Service;
using DOMAIN.DomainDal;
using DOMAIN.DomainRequest;
using Nethereum.Contracts.Standards.ERC20.TokenList;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL.Managers
{
	public sealed class NFTManager
	{
        #region singleton
        private readonly static NFTManager _instance = new NFTManager();

		public static NFTManager Current
		{
			get
			{
				return _instance;
			}
		}

		private NFTManager()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

        public static string contractAddress;

		public async Task<string> AddNFTBlockchainAsync(NFT nft, string id_user)
		{
            string trx = "";
            try
			{
				WalletAdmin walletAdmin = new WalletAdmin();
				walletAdmin = BilleteraManager.Current.GetWalletAdmin();
				Account account = NetConnection.Current.GetAccount(walletAdmin.privatekey);
				Web3 web3 = NetConnection.Current.Connection(walletAdmin.privatekey);
                var tokenIdToMint = int.Parse(nft.TokenNFTid); // ID del NFT que deseas crear
                var toAddress = nft.billetera; // Dirección a la que se enviará el nuevo NFT

                var mintNFTABI = @"function mintNFT(address to, uint256 tokenId) public";

                var contract = web3.Eth.GetContract(mintNFTABI, contractAddress);

                var mintFunction = contract.GetFunction("mintNFT");

                trx = await mintFunction.SendTransactionAsync(account.Address, gas: new HexBigInteger(200000), value: new HexBigInteger(0),
                                                           functionInput: new object[] { toAddress, tokenIdToMint });
				string detalle = "Se realiza el minteo del token con id:" + nft.TokenNFTid + "del usuario:" + id_user;
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");

                nft.TrxTransaccion = trx;
                TransactionReceipt _receipt = null;
                while (_receipt == null)
                {
                    _receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(trx);
                }
                nft.state_transaccion = _receipt.Status.Value.ToString();
                string detalleTransaccion = "Se consulto el estado de la transaccion:" + trx;
                BitacoraService.Current.AddBitacora("INFO", detalleTransaccion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");

                Transaccion transaccion = new Transaccion
                {
                    id_etherscan = nft.TrxTransaccion,
                    TokenIdNFT = nft.TokenNFTid,
                    usuario = GetNombreUser(id_user),
                    billetera_origen = nft.billetera,
                    billetera_destino = ""
                };
                if (nft.state_transaccion == "1")
                {
                    //se registra en la base
                    AddNFTDal(nft);
                    TransaccionManager.Current.AddTransaccionDal(transaccion);
                }
                if (nft.state_transaccion == "0")
                {
                    TransaccionManager.Current.AddTransaccionDal(transaccion);
                }
            }
			catch (Exception ex)
			{
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
			return trx;
		}

        public void AddNFTDal(NFT nft)
        {
			try
			{
				string addNft = "INSERT INTO [dbo].[Nft] (id_nft, TokenNFTid, billetera, estado, fecha_creacion) VALUES (@id_nft, @TokenNFTid, @billetera, @estado, @fecha_creacion)";
                SqlHelper.ExecuteNonQuery(addNft, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@id_nft", Guid.Parse(nft.id_nft)),
                    new SqlParameter("@TokenNFTid", int.Parse(nft.TokenNFTid)),
                    new SqlParameter("@billetera", nft.billetera),
                    new SqlParameter("@estado", nft.Estado),
                    new SqlParameter("@fecha_creacion", DateTime.Now)
                });
                string descripcion_addNft = "Se el registro en la base de datos el tokenNFT con id:" + nft.id_nft;
                BitacoraService.Current.AddBitacora("INFO", descripcion_addNft, "084757d9-cbf3-4098-9374-b9e6563dcfb3");

                string addInfoNft = "INSERT INTO [dbo].[informacion_nft] (id_informacion_paciente, TokenNFTid, Nombre_paciente, Apellido_paciente, Dni, Cobertura, Consulta, Patologia, fecha_creacion, fecha_modificacion) VALUES (@id_informacion_paciente, @TokenNFTid, @Nombre_paciente, @Apellido_paciente, @Dni, @Cobertura, @Consulta, @Patologia, @fecha_creacion, @fecha_modificacion)";
                SqlHelper.ExecuteNonQuery(addInfoNft, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@id_informacion_paciente", Guid.NewGuid()),
                    new SqlParameter("@TokenNFTid", int.Parse(nft.TokenNFTid)),
                    new SqlParameter("@Nombre_paciente", nft.Nombre_paciente),
                    new SqlParameter("@Apellido_paciente", nft.Apellido_paciente),
                    new SqlParameter("@Dni", int.Parse(nft.Dni)),
                    new SqlParameter("@Cobertura", nft.Cobertura),
                    new SqlParameter("@Consulta", nft.Consulta),
                    new SqlParameter("@Patologia", nft.Patologia),
                    new SqlParameter("@fecha_creacion", DateTime.Now),
                    new SqlParameter("@fecha_modificacion", DateTime.Now)
                });
                string descripcion_addInfoNft = "Se el registro en la base de datos la informacion del tokenNFT con id:" + nft.id_nft;
                BitacoraService.Current.AddBitacora("INFO", descripcion_addInfoNft, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
			catch (Exception ex)
			{
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }

        public async Task<string> TransferNFTBlockchain(NFT nft, string id_user, string billetera_destino)
		{
            string trx = "";
            try
            {
                WalletCompany walletCompany = new WalletCompany();
                walletCompany = BilleteraManager.Current.GetWalletCompany(id_user);
                Account account = NetConnection.Current.GetAccount(walletCompany.privateKey);
                Web3 web3 = NetConnection.Current.Connection(walletCompany.privateKey);
                var tokenId = int.Parse(nft.TokenNFTid);
                var toAddress = billetera_destino;

                var transaferABI = @"function transferNFT(address to, uint256 tokenId) public";

                var contract = web3.Eth.GetContract(transaferABI, contractAddress);

                var transferFunction = contract.GetFunction("transferNFT");

                trx = await transferFunction.SendTransactionAsync(account.Address, gas: new HexBigInteger(200000), value: new HexBigInteger(0),
                                                           functionInput: new object[] { toAddress, tokenId });

                string detalle = "Se realiza la transferencia del token con id:" + nft.TokenNFTid + "del usuario:" + id_user + "al usuario con la billetera:" + billetera_destino;
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");

                //time out 3 min
                nft.TrxTransaccion = trx;
                TransactionReceipt _receipt = null;
                while (_receipt == null)
                {
                    _receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(trx);
                }
                nft.state_transaccion = _receipt.Status.Value.ToString();
                string detalleTransaccion = "Se consulto el estado de la transaccion:" + trx;
                BitacoraService.Current.AddBitacora("INFO", detalleTransaccion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
                Transaccion transaccion = new Transaccion
                {
                    id_etherscan = nft.TrxTransaccion,
                    TokenIdNFT = nft.TokenNFTid,
                    usuario = GetNombreUser(id_user),
                    billetera_origen = walletCompany.wallet,
                    billetera_destino = billetera_destino
                };
                if (nft.state_transaccion == "1")
                {
                    TranferNFT(nft, billetera_destino);
                    TransaccionManager.Current.AddTransaccionDal(transaccion);
                }
                if (nft.state_transaccion == "0")
                {
                    TransaccionManager.Current.AddTransaccionDal(transaccion);
                }
            }
            catch (Exception ex) 
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return trx;
        }

        public void TranferNFT(NFT nft, string billetera_destino)
        {
            try
            {
                string statement = "UPDATE [dbo].[Nft] SET billetera = @billetera where TokenNFTid = @TokenNFTid";
                SqlHelper.ExecuteNonQuery(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@billetera", billetera_destino),
                    new SqlParameter("@TokenNFTid", int.Parse(nft.TokenNFTid))
                });
                string descripcion = "Se modifica el nft con tokenid:" + nft.TokenNFTid;
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }
        public async Task<string> TranferNFTWithETHBlockChain(NFT nft, string id_user, string billetera_destino, string id_user_Transfer)
        {
            string trx = "";
            try
            {
                WalletUser walletUser = new WalletUser();
                walletUser = BilleteraManager.Current.GetWalletUserAddress(nft.billetera);
                Account account = NetConnection.Current.GetAccount(walletUser.privateKey);
                Web3 web3 = NetConnection.Current.Connection(walletUser.privateKey);

                var tokenId = int.Parse(nft.TokenNFTid);
                var toAddress = billetera_destino;
                

                BigInteger weiPerEth = BigInteger.Parse("1000000000000000000"); // 10^18 wei por 1 ETH
                decimal precioEnEth = Convert.ToDecimal(nft.precio, CultureInfo.InvariantCulture);
                BigInteger weiAmount = BigInteger.Parse((Convert.ToUInt64(precioEnEth * (decimal)weiPerEth)).ToString());
                var ethValue = new HexBigInteger(weiAmount);

                string abi = @"function transferNFTWithETH(address to, uint256 tokenId) public payable returns (bytes32)";
                var contract = web3.Eth.GetContract(abi, contractAddress);

                var transferFunction = contract.GetFunction("transferNFTWithETH");

                trx = await transferFunction.SendTransactionAsync(account.Address, gas: new HexBigInteger(200000),
                                                                gasPrice: new HexBigInteger(5000000000), ethValue, 
                                                                functionInput: new object[] { toAddress, tokenId });

                string detalle = "Se realiza la transferencia del token con id:" + nft.TokenNFTid + "al usuario con la billetera:" + billetera_destino + "Con un pago de:" + nft.precio + "ETH";
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");

                //time out
                nft.TrxTransaccion = trx;
                TransactionReceipt _receipt = null;
                while (_receipt == null)
                {
                    _receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(trx);
                }
                nft.state_transaccion = _receipt.Status.Value.ToString();
                string detalleTransaccion = "Se consulto el estado de la transaccion:" + trx;
                BitacoraService.Current.AddBitacora("INFO", detalleTransaccion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
                Transaccion transaccion = new Transaccion
                {
                    id_etherscan = nft.TrxTransaccion,
                    TokenIdNFT = nft.TokenNFTid,
                    usuario = GetNombreUser(id_user),
                    billetera_origen = nft.billetera,
                    billetera_destino = billetera_destino
                };
                if (nft.state_transaccion == "1")
                {
                    TranferNFTWithETH(nft, billetera_destino);
                    TransaccionManager.Current.AddTransaccionDal(transaccion);
                }
                if (nft.state_transaccion == "0")
                {
                    TransaccionManager.Current.AddTransaccionDal(transaccion);
                }
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return trx;
        }

        public void TranferNFTWithETH(NFT nft, string billetera_destino)
        {
            try
            {
                string statement = "UPDATE [dbo].[Nft] SET estado = @estado, precio = @precio, billetera = @billetera where TokenNFTid = @TokenNFTid";
                SqlHelper.ExecuteNonQuery(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@billetera", billetera_destino),
                    new SqlParameter("@TokenNFTid", int.Parse(nft.TokenNFTid)),
                    new SqlParameter("@estado", ""),
                    new SqlParameter("@precio", ""),
                });
                string descripcion = "Se modifica el nft con tokenid:" + nft.TokenNFTid + "Y se envia a la billetera:" + billetera_destino;
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
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
				string statement = "select N.TokenNFTid, i.Nombre_paciente, i.Apellido_paciente, i.Dni, i.Cobertura, i.Consulta, i.Patologia, N.estado, N.precio from Nft as N join informacion_nft as i on i.TokenNFTid = N.TokenNFTid where N.TokenNFTid = @TokenNFTid";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                     new SqlParameter("@TokenNFTid", int.Parse(tokenid)),
                }))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        request = NFTAdapter.Current.adapt(vs);
                    }
                }
                string detalle = "Se obtienen la informacion del nft con id:" + tokenid;
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");

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
                string statement = "select bu.id_usuario, N.TokenNFTid, i.Nombre_paciente, i.Apellido_paciente, i.Dni, i.Cobertura, i.Consulta, i.Patologia, N.estado, N.precio from Nft as N join informacion_nft as i on i.TokenNFTid = N.TokenNFTid join Billetera as b on b.address = N.billetera join BilleteraUsuario as bu on bu.id_billetera = b.id_billetera where N.TokenNFTid = @TokenNFTid";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                     new SqlParameter("@TokenNFTid", int.Parse(tokenid)),
                }))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        request = NFTAdapter.Current.adaptGetNFT(vs);
                    }
                }
                string detalle = "Se obtienen la informacion del nft con id:" + tokenid;
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");

            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return request;
        }

        public IEnumerable<NftRequest> GetNFTList()
        {
            List<NftRequest> nftRequests = new List<NftRequest>();
            try
            {
                NftRequest nft = new NftRequest();
                string statement = "select N.TokenNFTid, i.Nombre_paciente, i.Apellido_paciente, i.Dni, i.Cobertura, i.Consulta, i.Patologia, N.estado, N.precio from Nft as N join informacion_nft as i on i.TokenNFTid = N.TokenNFTid";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        nft = NFTAdapter.Current.adapt(vs);
                        nftRequests.Add(nft);
                    }
                }
                string detalle = "Se obtienen una lista de los nft";
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return nftRequests;
        }

        public IEnumerable<NftRequest> GetNFTMarketplace(string cuitEmpresa)
		{
            List<NftRequest> nftRequests = new List<NftRequest>();
            try
            {
                NftRequest nft = new NftRequest();
                string statement = "select N.TokenNFTid, i.Nombre_paciente, i.Apellido_paciente, i.Dni, i.Cobertura, i.Consulta, i.Patologia, N.estado, N.precio from Nft as N join informacion_nft as i on i.TokenNFTid = N.TokenNFTid join Billetera as b on b.address = N.billetera join BilleteraUsuario as bu on bu.id_billetera = b.id_billetera join medico_empresa as me on me.id_usuario_medico = bu.id_usuario join Empresa as e on e.id_empresa = me.id_empresa where N.estado = @estado and e.cuit_empresa = @cuit_empresa";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                     new SqlParameter("@estado", "market"),
                     new SqlParameter("@cuit_empresa", cuitEmpresa),
                }))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        nft = NFTAdapter.Current.adapt(vs);
                        nftRequests.Add(nft);
                    }
                }
                string detalle = "Se obtienen una lista de los nft que se encuentran publicados en el market";
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return nftRequests;
        }

		public IEnumerable<NftRequest> GetNFTWallet(string billetera)
		{
            List<NftRequest> nftRequests = new List<NftRequest>();
            try
            {
                NftRequest nft = new NftRequest();
                string statement = "select N.TokenNFTid, i.Nombre_paciente, i.Apellido_paciente, i.Dni, i.Cobertura, i.Consulta, i.Patologia, N.estado, N.precio from Nft as N join informacion_nft as i on i.TokenNFTid = N.TokenNFTid where N.billetera = @billetera";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                     new SqlParameter("@billetera", billetera),
                }))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        nft = NFTAdapter.Current.adapt(vs);
                        nftRequests.Add(nft);
                    }
                }
                string detalle = "Se obtienen una lista de los nft que son de propiedad del nft:" + billetera;
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return nftRequests;
        }

		public void SellNFT(NFT nft)
		{
            try
            {
                string statement = "UPDATE [dbo].[Nft] SET precio = @precio, estado = @estado where TokenNFTid = @TokenNFTid";
                SqlHelper.ExecuteNonQuery(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@precio", nft.precio),
                    new SqlParameter("@estado", nft.Estado),
                    new SqlParameter("@TokenNFTid", int.Parse(nft.TokenNFTid))
                });
                string descripcion = "Se modifica el nft con tokenid:" + nft.TokenNFTid;
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");

            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }

        public void modifyInformacionNFT(NFT nft)
        {
            try
            {
                string statement = "UPDATE [dbo].[informacion_nft] SET Nombre_paciente = @Nombre_paciente, Apellido_paciente = @Apellido_paciente, Dni = @Dni, Cobertura = @Cobertura, Consulta = @Consulta, Patologia = @Patologia, fecha_modificacion = @fecha_modificacion where TokenNFTid = @TokenNFTid";
                SqlHelper.ExecuteNonQuery(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@Nombre_paciente", nft.Nombre_paciente),
                    new SqlParameter("@Apellido_paciente", nft.Apellido_paciente),
                    new SqlParameter("@Dni", nft.Dni),
                    new SqlParameter("@Cobertura", nft.Cobertura),
                    new SqlParameter("@Consulta", nft.Consulta),
                    new SqlParameter("@Patologia", nft.Patologia),
                    new SqlParameter("@fecha_modificacion", DateTime.Now),
                    new SqlParameter("@TokenNFTid", int.Parse(nft.TokenNFTid))
                });
                string descripcion = "Se modifica la informacion del nft con tokenid:" + nft.TokenNFTid;
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");

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
    }
}