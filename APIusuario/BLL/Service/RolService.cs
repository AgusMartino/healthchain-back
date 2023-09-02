﻿using DAL.Manager;
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
            try
            {
                List<Rol> rols = new List<Rol>();
                rols = (List<Rol>)RolManager.Current.GetAll();
                return rols;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

        }

        public Rol GetRol(int name)
        {
            try
            {
                Rol rol = new Rol();
                string[] criterios = { "name" };
                string[] valores = { name.ToString() };
                rol = RolManager.Current.GetOne(criterios, valores);
                return rol;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }

}
