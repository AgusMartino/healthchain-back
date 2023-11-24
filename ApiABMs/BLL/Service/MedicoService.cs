using DAL.Manager;
using DAL.Tools.Service;
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
            List<Medico> lista = new List<Medico>();
            try
            {
                lista = (List<Medico>)MedicoManager.Current.GetMedicosEmpresa(cuit);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return lista;
        }

        public Medico GetMedico(string username)
        {
            Medico medico = new Medico();
            try
            {
                medico = MedicoManager.Current.GetMedico(username);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return medico;
        }
    }

}
