using DAL.Manager.Adapters;
using DAL.Tools.Service;
using DAL.Tools;
using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
	public sealed class MedicoEspecialidadManager
	{
        #region singleton
        private readonly static MedicoEspecialidadManager _instance = new MedicoEspecialidadManager();

		public static MedicoEspecialidadManager Current
		{
			get
			{
				return _instance;
			}
		}

		private MedicoEspecialidadManager()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

		public void AddEspecialidad(string id_usuario, string id_especialidad)
		{
            try
            {
                string sqlstatemen = "INSERT INTO [dbo].[medico_especialidad] (id_usuario, id_especialidad) VALUES (@id_usuario, @id_especialidad)";
                using (var dr = SqlHelper.ExecuteReader(sqlstatemen, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@id_usuario", id_usuario),
                    new SqlParameter("@id_especialidad", id_especialidad)
                }));
                string descripcion = "Se registra la especialidad con id:" + id_especialidad + "al medico con id:" + id_usuario;
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }
    }

}
