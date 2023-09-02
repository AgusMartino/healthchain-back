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
            string[] criterios = { "id" };
            string[] valores = { values[(int)Columns.usuario].ToString() };
            Usuario usuario = new Usuario
            {
                Id = Guid.Parse(values[(int)Columns.id_usuario].ToString()),
                user = values[(int)Columns.usuario].ToString(),
                password = values[(int)Columns.contraseña].ToString(),
                name = values[(int)Columns.nombre].ToString(),
                lastname = values[(int)Columns.apellido].ToString(),
                rol = RolManager.Current.GetOne(criterios , valores ),
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
            tipo_usuario
        }
    }

}
