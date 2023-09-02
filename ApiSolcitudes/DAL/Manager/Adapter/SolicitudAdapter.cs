﻿using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager.Adapter
{

    public sealed class SolicitudAdapter
    {
        #region singleton
        private readonly static SolicitudAdapter _instance = new SolicitudAdapter();

        public static SolicitudAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private SolicitudAdapter()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public Solicitud adapt(object[] values)
        {
            string[] criterios = { "id" };
            string[] valores = { values[(int)Columns.id_tipo_solicitud].ToString() };
            Solicitud solicitud = new Solicitud
            {
                id_solicitud = values[(int)Columns.id_solicitud].ToString(),
                cuit_empresa = values[(int)Columns.cuit_empresa].ToString(),
                id_usuario =values[(int)Columns.id_usuario].ToString(),
                tipo_Solicitud = Tipo_solicitudManager.Current.GetOne(criterios, valores),
                Descripcion = values[(int)Columns.Descripcion].ToString(),
                aprobado = bool.Parse(values[(int)Columns.aprobado].ToString()),
                fecha_creacion = DateTime.Parse(values[(int)Columns.fecha_creacion].ToString()),
                fecha_modificacion = DateTime.Parse(values[(int)Columns.fecha_modificacion].ToString())
            };
            return solicitud;
        }

        private enum Columns
        {
            id_solicitud,
            cuit_empresa,
            id_usuario,
            id_tipo_solicitud,
            Descripcion,
            aprobado,
            fecha_creacion,
            fecha_modificacion
        }
    }

}