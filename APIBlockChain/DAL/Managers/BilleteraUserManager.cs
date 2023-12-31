﻿using ADRaffy.ENSNormalize;
using DAL.Tools;
using DAL.Tools.Service;
using DOMAIN.DomainDal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Managers
{
	public sealed class BilleteraUserManager
    {
        #region singleton
        private readonly static BilleteraUserManager _instance = new BilleteraUserManager();

		public static BilleteraUserManager Current
		{
			get
			{
				return _instance;
			}
		}

		private BilleteraUserManager()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

        public void createRelationshipUserBillletera(WalletUser walletUser)
        {
            try
            {
                string statement = "INSERT INTO [dbo].[BilleteraUsuario] (id_billetera_usuario, id_billetera, id_usuario) VALUES (@id_billetera_usuario, @id_billetera, @id_usuario)";
                SqlHelper.ExecuteNonQuery(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@id_billetera_usuario", Guid.NewGuid()),
                    new SqlParameter("@id_billetera", Guid.Parse(walletUser.id_wallet)),
                    new SqlParameter("@id_usuario", Guid.Parse(walletUser.user_id)),
                });
                string detalle = "Se crea la relacion entre la billetera:" + walletUser.wallet + "y el usuario:" + walletUser.user_id;
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }

        }
    }

}
