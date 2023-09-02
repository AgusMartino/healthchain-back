using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager.Adapters
{
    public sealed class EmpresaAdapter
    {
        #region singleton
        private readonly static EmpresaAdapter _instance = new EmpresaAdapter();

        public static EmpresaAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private EmpresaAdapter()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public Empresa adapt(object[] values)
        {
            Empresa empresa = new Empresa
            {
                id_empresa = values[(int)Columns.id_empresa].ToString(),
                name = values[(int)Columns.nombre_empresa].ToString(),
                cuit = values[(int)Columns.cuit_empresa].ToString(),
                direccion = values[(int)Columns.direccion_empresa].ToString(),
                fecha_creacion = DateTime.Parse(values[(int)Columns.fecha_creacion].ToString()),
                fecha_modificacion = DateTime.Parse(values[(int)Columns.fecha_modificacion].ToString()),
            };
            return empresa;
        }

        private enum Columns
        {
            id_empresa,
            cuit_empresa,
            nombre_empresa,
            direccion_empresa,
            fecha_creacion,
            fecha_modificacion
        }
    }

}
