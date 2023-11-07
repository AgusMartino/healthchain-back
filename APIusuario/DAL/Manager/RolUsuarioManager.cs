using DAL.Interface;
using DAL.Manager.Adapter;
using DAL.Tools;
using DAL.Tools.Service;
using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
    public sealed class RolUsuarioManager : IGenericRelationship<Usuario, Rol>
    {
        #region singleton
        private readonly static RolUsuarioManager _instance = new RolUsuarioManager();

        public static RolUsuarioManager Current
        {
            get
            {
                return _instance;
            }
        }

        private RolUsuarioManager()
        {
            //Implent here the initialization of your singleton
        }
        #endregion
        public void Join(Usuario obj1, Rol obj2)
        {
            string statement = "INSERT INTO usuario_rol(IdUsuario,IdRol) values (@IdUsuario, @IdRol)";
            try
            {
                SqlHelper.ExecuteNonQuery(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@IdUsuario", Guid.Parse(obj1.id)),
                    new SqlParameter("@IdRol", Convert.ToInt32(obj2.Id))
                });
                string descripcion = "Se le asigno el rol con id" + obj2.Id + "al usuario con id:" + obj1.id;
                BitacoraService.Current.AddBitacora("INFO", descripcion, obj1.id);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.ToString(), obj1.id);
            }
        }

        public Rol GetRol(string id)
        {
            Rol rol = new Rol();
            try
            {
                string statement = "Select * from Rol as r join usuario_rol as ur on ur.IdRol = r.Id_Rol where ur.IdUsuario = @id_usuario";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[] {
                                                       new SqlParameter("@id_usuario", Guid.Parse(id))}))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        rol = RolAdapter.Current.adapt(vs);
                    }
                }
                string descripcion = "se obtuvo el rol con id:" + id;
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return rol;

        }

        public void delete(Usuario obj1, Rol obj2)
        {
            string statement = "DELETE FROM [dbo].[usuario_rol] WHERE IdUsuario = @IdUsuario";
            try
            {
                SqlHelper.ExecuteNonQuery(statement, System.Data.CommandType.Text, new SqlParameter("@IdUsuario", Guid.Parse(obj1.id)));
                string descripcion = "Se elimino la relacion entre el usuario con id:" + obj1.id + "con el rol con id:" + obj2.Id;
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }

        public Rol GetOne(Guid obj)
        {
            throw new NotImplementedException();
        }
    }

}
