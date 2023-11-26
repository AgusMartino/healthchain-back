using DOMAIN.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Adapter
{
	public sealed class BitacoraRequestAdapter
    {
        #region singleton
        private readonly static BitacoraRequestAdapter _instance = new BitacoraRequestAdapter();

		public static BitacoraRequestAdapter Current
		{
			get
			{
				return _instance;
			}
		}

		private BitacoraRequestAdapter()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

        public BitacoraRequest adapt(object[] values)
        {
            BitacoraRequest bitacoraRequest = new BitacoraRequest
            {
                id_bitacora = values[(int)Columns.id_log].ToString(),
                id_usuario = values[(int)Columns.id_usuario].ToString(),
                name = values[(int)Columns.name].ToString(),
                lastname = values[(int)Columns.lastname].ToString(),
                description = values[(int)Columns.description].ToString(),
                type = values[(int)Columns.type].ToString(),
                creation_date = values[(int)Columns.creation_date].ToString()

            };
            return bitacoraRequest;
        }

        private enum Columns
        {
            id_log,
            id_usuario,
            name,
            lastname,
            description,
            type,
            creation_date
        }
    }

}
