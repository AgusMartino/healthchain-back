using DLL.Manager;
using DOMAIN.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{

	public sealed class BitacoraService
	{
        #region singleton
        private readonly static BitacoraService _instance = new BitacoraService();

		public static BitacoraService Current
		{
			get
			{
				return _instance;
			}
		}

		private BitacoraService()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

		public void AddBitacora(BitacoraRequest bitacora)
		{
			try
			{
				Bitacora _bitacora = new Bitacora
				{
					id_bitacora = Guid.NewGuid().ToString(),
					id_usuario = bitacora.id_usuario,
					description = bitacora.description,
					type = bitacora.type,
					creattion_date = DateTime.Now.ToString()
				};
				BitacoraManager.Current.AddBitacora(_bitacora);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

        public List<BitacoraRequest> GetBitacora( FechasRequest fechasRequest)
        {
            try
            {
				List<BitacoraRequest> bitacoraRequests = new List<BitacoraRequest>();
				bitacoraRequests = BitacoraManager.Current.GetBitacora(fechasRequest.fecha_de_incio, fechasRequest.fecha_de_fin);
				return bitacoraRequests;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
