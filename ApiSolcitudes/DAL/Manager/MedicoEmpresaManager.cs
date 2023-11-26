using DAL.Tools.Service;
using DAL.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
	public sealed class MedicoEmpresaManager
	{
        #region singleton
        private readonly static MedicoEmpresaManager _instance = new MedicoEmpresaManager();

		public static MedicoEmpresaManager Current
		{
			get
			{
				return _instance;
			}
		}

		private MedicoEmpresaManager()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

		public void RelationshipMedicoEmpresa(string id_medico, string id_empresa)
		{
            try
            {
                string statement = "INSERT INTO [dbo].[medico_empresa] (id_usuario_medico, id_empresa) VALUES (@id_usuario_medico, @id_empresa)";
                SqlHelper.ExecuteNonQuery(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@id_usuario_medico", Guid.Parse(id_medico)),
                    new SqlParameter("@id_empresa", Guid.Parse(id_empresa))
                });

            string descripcion = "Se genero la relacion entre las empresa con id" + id_empresa + " y el usuario medico con id: " + id_medico;
            BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }
    }

}
