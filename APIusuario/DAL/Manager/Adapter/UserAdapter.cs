using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager.Adapter
{

    public sealed class UserAdapter
    {
        #region singleton
        private readonly static UserAdapter _instance = new UserAdapter();

        public static UserAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private UserAdapter()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public Usuario adapt(object[] values)
        {
            Usuario usuario = new Usuario
            {
                id = values[(int)Columns.id_usuario].ToString(),
                user = values[(int)Columns.usuario].ToString(),
                password = values[(int)Columns.contraseña].ToString(),
                name = values[(int)Columns.nombre].ToString(),
                lastname = values[(int)Columns.apellido].ToString(),
                user_type = values[(int)Columns.tipo_usuario].ToString(),
                cuit_empresa = values[(int)Columns.cuit_empresa].ToString(),
                rol = RolUsuarioManager.Current.GetRol(values[(int)Columns.id_usuario].ToString()),
                fecha_creacion = DateTime.Parse(values[(int)Columns.fecha_creacion].ToString()),
                fecha_modificacion = DateTime.Parse(values[(int)Columns.fecha_modificacion].ToString())
            };
            return usuario;
        }

        private enum Columns
        {
            id_usuario,
            usuario,
            contraseña,
            nombre,
            apellido,
            cuit_empresa,
            tipo_usuario,
            fecha_creacion,
            fecha_modificacion
        }
    }

}
