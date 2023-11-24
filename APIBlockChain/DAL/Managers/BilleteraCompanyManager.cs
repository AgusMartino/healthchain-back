using DAL.Tools;
using DAL.Tools.Service;
using DOMAIN.DomainDal;
using Nethereum.HdWallet;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Managers
{
	public sealed class BilleteraCompanyManager
    {
        #region singleton
        private readonly static BilleteraCompanyManager _instance = new BilleteraCompanyManager();

		public static BilleteraCompanyManager Current
		{
			get
			{
				return _instance;
			}
		}

		private BilleteraCompanyManager()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

		public void createRelationshipCompanyBillletera(WalletCompany walletCompany)
		{
			try
			{
                string statement = "INSERT INTO [dbo].[BilleteraEmpresa] (id_billetera_empresa, id_billetera, id_empresa) VALUES (@id_billetera_empresa, @id_billetera, @id_empresa)";
                SqlHelper.ExecuteNonQuery(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                new SqlParameter("@id_billetera_empresa", Guid.NewGuid()),
                new SqlParameter("@id_billetera", Guid.Parse(walletCompany.id_wallet)),
                new SqlParameter("@id_empresa", Guid.Parse(walletCompany.company_id)),
                });
                string detalle = "Se crea la relacion entre la billetera:" + walletCompany.wallet + "y el usuario:" + walletCompany.company_id;
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }

        }
    }

}
