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
                Empresa empresa = EmpresaService.Current.GetOne(Convert.ToInt32(cuit));
                List<Medico> lista = new List<Medico>();
                lista = (List<Medico>)MedicoManager.Current.GetMedicosEmpresa(empresa.id_empresa);
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}
