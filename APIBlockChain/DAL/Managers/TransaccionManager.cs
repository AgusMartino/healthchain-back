using ADRaffy.ENSNormalize;
using DAL.Adapter;
using DAL.Tools;
using DAL.Tools.Service;
using DOMAIN.DomainDal;
using Nethereum.HdWallet;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Managers
{
	public sealed class TransaccionManager
	{
        #region singleton
        private readonly static TransaccionManager _instance = new TransaccionManager();

		public static TransaccionManager Current
		{
			get
			{
				return _instance;
			}
		}

		private TransaccionManager()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

		public async Task<string> EstadoTransaccionAsync(string transaccion)
		{
			var receipt = "";
			try
			{
				WalletAdmin wallet = BilleteraManager.Current.GetWalletAdmin();
				Web3 web3 = NetConnection.Current.Connection(wallet.privatekey);

				var _receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transaccion);
				receipt = _receipt.Status.Value.ToString();
				string detalle = "Se consulto el estado de la transaccion:" + transaccion;
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
			catch (Exception ex)
			{
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
			return receipt;
		}

		public void AddTransaccionDal(Transaccion transaccion)
		{
			try
			{
				string statement = "INSERT INTO [dbo].[transaccion] (id_trasaccion, id_etherscan, TokenIdNFT, usuario, billetera_origen, billetera_destino, fecha_transaccion) VALUES (@id_trasaccion, @id_etherscan, @TokenIdNFT, @usuario, @billetera_origen, @billetera_destino, @fecha_transaccion)";
                SqlHelper.ExecuteNonQuery(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@id_trasaccion", Guid.NewGuid),
                    new SqlParameter("@id_etherscan", transaccion.id_etherscan),
                    new SqlParameter("@TokenIdNFT", int.Parse(transaccion.TokenIdNFT)),
                    new SqlParameter("@usuario", transaccion.usuario),
                    new SqlParameter("@billetera_origen", transaccion.billetera_origen),
                    new SqlParameter("@billetera_destino", transaccion.billetera_destino),
                    new SqlParameter("@fecha_transaccion", DateTime.Now)
                });
                string detalle = "Se registro la transaccion:" + transaccion + "en la base de datos";
				BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
			catch (Exception ex)
			{
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
		}
		public Transaccion getTransaccion(string TrxTransaccion)
		{
			Transaccion transaccion = new Transaccion();
			try
			{
                string statement = "Select id_etherscan, TokenIdNFT, usuario, billetera_origen, billetera_destino, fecha_transaccion From transaccion where id_etherscan = @id_etherscan";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                     new SqlParameter("@id_etherscan", TrxTransaccion),
                }))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        transaccion = TransaccionAdapter.Current.adapt(vs);
                    }
                }
                string detalle = "Se obtiene la informacion de la transaccion:" + TrxTransaccion;
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
			catch (Exception ex)
			{
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
			return transaccion;
		}
		public IEnumerable<Transaccion> getListTransacciones()
		{
			List<Transaccion> transaccions = new List<Transaccion>();
			try 
			{
                Transaccion transaccion = new Transaccion();
                string statement = "Select id_etherscan, TokenIdNFT, usuario, billetera_origen, billetera_destino, fecha_transaccion From transaccion";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        transaccion = TransaccionAdapter.Current.adapt(vs);
                        transaccions.Add(transaccion);
                    }
                }
                string detalle = "Se obtienen todas las transacciones";
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
			catch (Exception ex)
			{
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
			return transaccions;
		}
        public IEnumerable<Transaccion> getListTransaccionesUser(string user)
        {
            List<Transaccion> transaccions = new List<Transaccion>();
            try
            {
                Transaccion transaccion = new Transaccion();
                string statement = "Select id_etherscan, TokenIdNFT, usuario, billetera_origen, billetera_destino, fecha_transaccion From transaccion where usuario = @usuario";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                     new SqlParameter("@usuario", user),
                }))
                { 
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        transaccion = TransaccionAdapter.Current.adapt(vs);
                        transaccions.Add(transaccion);
                    }
                }
                string detalle = "Se obtienen todas las transacciones del usuario:" + user;
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return transaccions;
        }

        public IEnumerable<Transaccion> getListTransaccionesFecha(string fechaInicio, string fechaFin)
        {
            List<Transaccion> transaccions = new List<Transaccion>();
            try
            {
                Transaccion transaccion = new Transaccion();
                string statement = "Select id_etherscan, TokenIdNFT, usuario, billetera_origen, billetera_destino, fecha_transaccion From transaccion where fecha_transaccion BETWEEN @fecha_incio and @fecha_final";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                     new SqlParameter("@fecha_incio", DateTime.Parse(fechaInicio)),
                     new SqlParameter("@fecha_final", DateTime.Parse(fechaFin))
                }))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        transaccion = TransaccionAdapter.Current.adapt(vs);
                        transaccions.Add(transaccion);
                    }
                }
                string detalle = "Se obtienen todas las transacciones entre las fechas:" + fechaInicio + "y la fecha:" + fechaFin;
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return transaccions;
        }

        public IEnumerable<Transaccion> getListTransaccionesFechaUser(string fechaInicio, string fechaFin, string user)
        {
            List<Transaccion> transaccions = new List<Transaccion>();
            try
            {
                Transaccion transaccion = new Transaccion();
                string statement = "Select id_etherscan, TokenIdNFT, usuario, billetera_origen, billetera_destino, fecha_transaccion From transaccion where fecha_transaccion BETWEEN @fecha_incio and @fecha_final and usuario = @usuario";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                     new SqlParameter("@fecha_incio", DateTime.Parse(fechaInicio)),
                     new SqlParameter("@fecha_final", DateTime.Parse(fechaFin)),
                     new SqlParameter("@usuario", user)
                }))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        transaccion = TransaccionAdapter.Current.adapt(vs);
                        transaccions.Add(transaccion);
                    }
                }
                string detalle = "Se obtienen todas las transacciones entre las fechas:" + fechaInicio + "y la fecha:" + fechaFin + "del usuario:" + user;
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return transaccions;
        }
        public IEnumerable<Transaccion> getListTransaccionesFechaCompany(string fechaInicio, string fechaFin, string billetera)
        {
            List<Transaccion> transaccions = new List<Transaccion>();
            try
            {
                Transaccion transaccion = new Transaccion();
                string statement = "Select id_etherscan, TokenIdNFT, usuario, billetera_origen, billetera_destino, fecha_transaccion From transaccion where fecha_transaccion BETWEEN @fecha_incio and @fecha_final and billetera_origen = @billetera_origen";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                     new SqlParameter("@fecha_incio", DateTime.Parse(fechaInicio)),
                     new SqlParameter("@fecha_final", DateTime.Parse(fechaFin)),
                     new SqlParameter("@billetera_origen", billetera)
                }))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        transaccion = TransaccionAdapter.Current.adapt(vs);
                        transaccions.Add(transaccion);
                    }
                }
                string detalle = "Se obtienen todas las transacciones entre las fechas:" + fechaInicio + "y la fecha:" + fechaFin + "de la billetera:" + billetera;
                BitacoraService.Current.AddBitacora("INFO", detalle, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return transaccions;
        }
    }

}
