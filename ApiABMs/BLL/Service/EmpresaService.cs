using DAL.Manager;
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

        public IEnumerable<Empresa> GetAllEmpresasAsociadasMedico(string username)
        {
            try
            {
                List<Empresa> empresas = new List<Empresa>();
                string userid = GetUserId(username);
                empresas = (List<Empresa>)EmpresaManager.Current.GetAllEmpresasAsociadasMedico(userid);
                return empresas;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string GetUserId(string username)
        {
            string userId = "";
            using (var client = new HttpClient())
            {
                string url = "https://localhost:7151/api/User/GetUser/" + username;
                client.DefaultRequestHeaders.Clear();
                var response = client.GetAsync(url).Result;
                var res = response.Content.ReadAsStringAsync().Result;
                dynamic r = JObject.Parse(res);
                userId = r[0];
            }
            return userId;
        }
    }

}
