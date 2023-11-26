using DAL.Manager;
using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using DAL.Tools.Service;

namespace BLL.Service
{

    public sealed class SolicitudService
    {
        #region singleton
        private readonly static SolicitudService _instance = new SolicitudService();

        public static SolicitudService Current
        {
            get
            {
                return _instance;
            }
        }

        private SolicitudService()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public Solicitud GetOne(string cuit, Guid usuario)
        {
            Solicitud solicitud = new Solicitud();
            try
            {
                string[] criterios = { "cuit_empresa", "id_usuario" };
                string[] valores = { cuit.ToString(), usuario.ToString() };
                solicitud = SolicitudManager.Current.GetOne(criterios, valores);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return solicitud;
        }

        public void Add(Solicitud solicitud)
        {
            try
            {
                solicitud.id_solicitud = Guid.NewGuid().ToString();
                solicitud.fecha_creacion = DateTime.Now;
                solicitud.fecha_modificacion = DateTime.Now;
                SolicitudManager.Current.Add(solicitud);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            
        }

        public void Updtae(Solicitud solicitud)
        {
            try
            {
                solicitud.fecha_modificacion = DateTime.Now;
                SolicitudManager.Current.Update(solicitud);
                if(solicitud.estado == "2")
                {
                    string id_empresa = GetIDEmpresa(solicitud.cuit_empresa);
                    MedicoEmpresaManager.Current.RelationshipMedicoEmpresa(solicitud.id_usuario, id_empresa);
                }
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }

        public IEnumerable<Solicitud> GetAll(int tipo, string cuit)
        {
            List<Solicitud> solicituds = new List<Solicitud>();
            try
            {
                solicituds = (List<Solicitud>)SolicitudManager.Current.GetAllSolicitudes(tipo, cuit);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return solicituds;
        }

        public IEnumerable<Solicitud> GetAllSolicitudesUser(string id_usuario)
        {
            List<Solicitud> solicituds = new List<Solicitud>();
            try
            {
                solicituds = (List<Solicitud>)SolicitudManager.Current.GetAllSolicitudesUser(id_usuario);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return solicituds;
        }

        public string GetIDEmpresa(string cuit)
        {
            string id = "";
            using (var clientHandler = new HttpClientHandler())
            {
                string url = "https://localhost:7227/api/Empresa/GetOneEmpresa/" + cuit;
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);
                client.DefaultRequestHeaders.Clear();
                var response = client.GetAsync(url).Result;
                var res = response.Content.ReadAsStringAsync().Result;
                dynamic r = JObject.Parse(res);
                id = Convert.ToString(r["id_empresa"]);
            }
            return id;
        }
    }

}
