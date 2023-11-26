using DAL.Manager;
using DAL.Tools.Service;
using Domain.DOMAIN;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL.Service
{
    public sealed class EmpresaService
    {
        #region singleton
        private readonly static EmpresaService _instance = new EmpresaService();

        public static EmpresaService Current
        {
            get
            {
                return _instance;
            }
        }

        private EmpresaService()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public void Add(Empresa empresa)
        {
            try
            {
                empresa.id_empresa = Guid.NewGuid().ToString();
                empresa.fecha_creacion = DateTime.Now;
                empresa.fecha_modificacion = DateTime.Now;
                EmpresaManager.Current.Add(empresa);
                BilleteraService.Current.AddBilletera(empresa.id_empresa);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }
        
        public Empresa GetOne(string cuit)
        {
            string[] criterios = { "cuit" };
            string[] valores = { cuit };
            Empresa empresa = new Empresa();
            try
            {
                empresa = EmpresaManager.Current.GetOne(criterios, valores);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return empresa;
        }

        public IEnumerable<Empresa> GetAll()
        {
            List<Empresa> empresas = new List<Empresa>();
            try
            {
                empresas = (List<Empresa>)EmpresaManager.Current.GetAll(); 
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return empresas;
        }

        public IEnumerable<Empresa> GetAllEmpresasAsociadasMedico(string userid)
        {
            List<Empresa> empresas = new List<Empresa>();
            try
            {
                empresas = (List<Empresa>)EmpresaManager.Current.GetAllEmpresasAsociadasMedico(userid);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return empresas;
        }
    }

}
