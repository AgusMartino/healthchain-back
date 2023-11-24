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
    public sealed class RolService
    {
        #region singleton
        private readonly static RolService _instance = new RolService();

        public static RolService Current
        {
            get
            {
                return _instance;
            }
        }

        private RolService()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public List<Rol> GetRols()
        {
            List<Rol> rols = new List<Rol>();
            try
            {
                rols = (List<Rol>)RolManager.Current.GetAll(); 
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return rols;


        }

        public Rol GetRol(int id)
        {
            Rol rol = new Rol();
            try
            {
                string[] criterios = { "id" };
                string[] valores = { id.ToString() };
                rol = RolManager.Current.GetOne(criterios, valores);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return rol;
        }

    }

}
