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
using DAL.Tools.Service;
using DAL.Adapter;

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
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
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
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }

        public WalletUser GetWalletUser(string user_Id)
        {
            WalletUser walletUser = new WalletUser();
            try
            {
                string statement = "Select b.id_billetera, b.address, b.privateKey, u.id_usuario from usuario as u join BilleteraUsuario as bu on bu.id_usuario = u.id_usuario join Billetera as b on b.id_billetera = bu.id_billetera where u.id_usuario = @id_usuario";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                     new SqlParameter("@id_usuario", Guid.Parse(user_Id)),
                }))
                {
                    if (dr.Read())
                    {
                        object[] values = new object[dr.FieldCount];
                        dr.GetValues(values);
                        walletUser = WalletUserAdapter.Current.adapt(values);
                    }
                };
                string detalle = "Se obtiene la informacion de la billetera perteneciente al usuario con id:" + user_Id;
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return walletUser;
        }

        public WalletCompany GetWalletCompany(string user_Id)
        {
            WalletCompany walletCompany = new WalletCompany();
            try
            {
                string statement = "Select b.id_billetera, b.address, b.privateKey, e.id_empresa from usuario as u join Empresa as e on e.cuit_empresa = u.cuit_empresa join BilleteraEmpresa as be on be.id_empresa = e.id_empresa join Billetera as b on b.id_billetera = be.id_billetera where u.id_usuario = @id_usuario";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                     new SqlParameter("@id_usuario", Guid.Parse(user_Id)),
                }))
                { 
                    if (dr.Read())
                    {
                        object[] values = new object[dr.FieldCount];
                        dr.GetValues(values);
                        walletCompany = WalletCompanyAdapter.Current.adapt(values);
                    }
                };
                string detalle = "Se obtiene la informacion de la billetera empresa a la que pertenece el usuario con id:" + user_Id;
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return walletCompany;
        }

        public WalletAdmin GetWalletAdmin()
        {
            WalletAdmin walletAdmin = new WalletAdmin();
            try
            {
                string statement = "Select address, privateKey from Billetera where id_billetera = 'e4c588c0-487f-45b5-a4d1-9f68502e407c'";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text))
                {
                    if (dr.Read())
                    {
                        object[] values = new object[dr.FieldCount];
                        dr.GetValues(values);
                        walletAdmin = WalletAdminAdapter.Current.adapt(values);
                    }
                };
                string detalle = "Se obtiene la informacion de la billetera Admin";
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return walletAdmin;
        }

        public WalletUser GetWalletUserAddress(string billetera)
        {
            WalletUser walletUser = new WalletUser();
            try
            {
                string statement = "Select b.id_billetera, b.address, b.privateKey, u.id_usuario from usuario as u join BilleteraUsuario as bu on bu.id_usuario = u.id_usuario join Billetera as b on b.id_billetera = bu.id_billetera where b.address = @address";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                     new SqlParameter("@address", billetera),
                }))
                {
                    if (dr.Read())
                    {
                        object[] values = new object[dr.FieldCount];
                        dr.GetValues(values);
                        walletUser = WalletUserAdapter.Current.adapt(values);
                    }
                };
                string detalle = "Se obtiene la informacion de la billetera:" + billetera;
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return walletUser;
        }
    }

}
