using DAL.Manager;
using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public sealed class MedicoService
    {
        #region singleton
        private readonly static MedicoService _instance = new MedicoService();

        public static MedicoService Current
        {
            get
            {
                return _instance;
            }
        }

        private MedicoService()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public IEnumerable<Medico> getAllmedicosEmpresas(string cuit)
        {
            try
            {
                List<Medico> lista = new List<Medico>();
                lista = (List<Medico>)MedicoManager.Current.GetMedicosEmpresa(cuit);
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Medico GetMedico(string username)
        {
            try
            {
                Medico medico = new Medico();
                medico = MedicoManager.Current.GetMedico(username);
                return medico;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}
