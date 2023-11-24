using DOMAIN.DomainDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Adapter
{
	public sealed class TransaccionAdapter
	{
        #region singleton
        private readonly static TransaccionAdapter _instance = new TransaccionAdapter();

		public static TransaccionAdapter Current
		{
			get
			{
				return _instance;
			}
		}

		private TransaccionAdapter()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

        public Transaccion adapt(object[] values)
        {
            Transaccion transaccion = new Transaccion
            {
                id_etherscan = values[(int)Columns.id_etherscan].ToString(),
                TokenIdNFT = values[(int)Columns.TokenIdNFT].ToString(),
                usuario = values[(int)Columns.usuario].ToString(),
                billetera_origen = values[(int)Columns.billetera_origen].ToString(),
                billetera_destino = values[(int)Columns.billetera_destino].ToString(),
                fecha_transaccion = values[(int)Columns.fecha_transaccion].ToString()
            };
            return transaccion;
        }
        private enum Columns
        {
            id_etherscan,
            TokenIdNFT,
            usuario,
            billetera_origen,
            billetera_destino,
            fecha_transaccion
        }
    }

}
