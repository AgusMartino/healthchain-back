using DAL.Tools;
using DLL.Adapter;
using DOMAIN.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Manager
{
	public sealed class BitacoraManager
	{
		#region singleton
		private readonly static BitacoraManager _instance = new BitacoraManager();

		public static BitacoraManager Current
		{
			get
			{
				return _instance;
			}
		}

		private BitacoraManager()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

		public void AddBitacora(Bitacora bitacora)
		{
			string statement = "INSERT INTO [dbo].[Log] (id_log, id_usuario, tipo_de_log, Descripcion, creation_date) VALUES (@id_log, @id_usuario, @tipo_de_log, @Descripcion, @creation_date)";
            SqlHelper.ExecuteNonQuery(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@id_log", Guid.Parse(bitacora.id_bitacora)),
                    new SqlParameter("@id_usuario", Guid.Parse(bitacora.id_usuario)),
                    new SqlParameter("@tipo_de_log", bitacora.type),
                    new SqlParameter("@Descripcion", bitacora.description),
                    new SqlParameter("@creation_date", DateTime.Parse(bitacora.creattion_date))
                });
        }

        public List<BitacoraRequest> GetBitacora(string fecha_incio, string fecha_final)
        {
			try
			{
				List<BitacoraRequest> bitacoraRequests = new List<BitacoraRequest>();
                string statement = "Select l.id_log, l.id_usuario, u.nombre, u.apellido, l.Descripcion, l.tipo_de_log, l.creation_date from dbo.Log as l join usuario as u on l.id_usuario = u.id_usuario where l.creation_date BETWEEN @fecha_incio and @fecha_final";
				BitacoraRequest bitacoraRequest = new BitacoraRequest();
				using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[]
				{
					new SqlParameter("@fecha_incio", DateTime.Parse(fecha_incio)),
                    new SqlParameter("@fecha_final", DateTime.Parse(fecha_final))
                }))
				{
					while (dr.Read())
					{
						object[] vs = new object[dr.FieldCount];
						dr.GetValues(vs);
                        bitacoraRequest = BitacoraRequestAdapter.Current.adapt(vs);
                        bitacoraRequests.Add(bitacoraRequest);
					}
				}
                return bitacoraRequests;
			}
			catch (Exception ex)
			{
				throw ex;
			}
        }
    }

}
