using DAL.Manager.Adapters;
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

    public sealed class MedicoManager
    {
        #region singleton
        private readonly static MedicoManager _instance = new MedicoManager();

        public static MedicoManager Current
        {
            get
            {
                return _instance;
            }
        }

        private MedicoManager()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public IEnumerable<Medico> GetMedicosEmpresa(string guidEmpresa)
        {
            try
            {
                List<Medico> medicos = new List<Medico>();
                Medico medico = new Medico();
                string sqlstatemen = "SELECT u.nombre, u.apellido, u.usuario, e.especialidad"
                                    + "from medico_empresa as me"
                                    + "join usuario as u on u.id_usuario = me.id_medico_empresa"
                                    + "join medico_especialidad as m on m.id_usuario = u.id_usuario"
                                    + "join especialidades as e on e.id_especialidad = m.id_especialidad"
                                    + "where me.id_empresa = @empresa";
                using (var dr = SqlHelper.ExecuteReader(sqlstatemen, System.Data.CommandType.Text, new SqlParameter[]
                {
                new SqlParameter("@empresa", Guid.Parse(guidEmpresa))
                }))
                {
                    if (dr.Read())
                    {
                        object[] values = new object[dr.FieldCount];
                        dr.GetValues(values);
                        medico = MedicoAdapter.Current.adapt(values);
                        medicos.Add(medico);
                    }
                }
                return medicos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        
    }

}
