using DAL.Manager;
using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                EmpresaManager.Current.Add(empresa);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        
        public Empresa GetOne(int cuit)
        {
            string[] criterios = { "cuit" };
            string[] valores = { cuit.ToString() };
            Empresa empresa = new Empresa();
            try
            {
                empresa = EmpresaManager.Current.GetOne(criterios, valores);
                return empresa;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Empresa> GetAll()
        {
            try
            {
                List<Empresa> empresas = new List<Empresa>();
                empresas = (List<Empresa>)EmpresaManager.Current.GetAll();
                return empresas;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}
