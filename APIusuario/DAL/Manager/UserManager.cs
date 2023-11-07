using DAL.Interface;
using DAL.Manager.Adapter;
using DAL.Tools;
using DAL.Tools.Service;
using Domain.DOMAIN;
using System.Data.SqlClient;

namespace DAL.Manager
{
    public sealed class UserManager : IGenericRepository<Usuario>
    {
        #region singleton
        private readonly static UserManager _instance = new UserManager();

        public static UserManager Current
        {
            get
            {
                return _instance;
            }
        }

        private UserManager()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Usuario] (id_usuario, usuario, contraseña, nombre, apellido, tipo_usuario, cuit_empresa, fecha_creacion, fecha_modificacion) VALUES (@id_usuario, @usuario, @contraseña, @nombre, @apellido, @tipo_usuario, @cuit_empresa, @fecha_creacion, @fecha_modificacion)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Usuario] SET usuario = @usuario, contraseña = @contraseña, nombre = @nombre, apellido = @apellido, tipo_usuario = @tipo_usuario, cuit_empresa = @cuit_empresa, fecha_modificacion = @fecha_modificacion WHERE id_usuario = @id_usuario";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Usuario] WHERE  IdUsuario = @IdUsuario";
        }

        private string SelectOneStatement
        {
            get => "SELECT * FROM [dbo].[Usuario] WHERE usuario = @usuario";
        }

        private string SelectAllStatement
        {
            get => "SELECT * FROM [dbo].[Usuario]";
        }
        #endregion

        public Usuario GetUserByUser(string user)
        {
            Usuario usuario = new Usuario();
            try
            {
                using (var dr = SqlHelper.ExecuteReader(SelectOneStatement, System.Data.CommandType.Text, new SqlParameter[] {
                                                       new SqlParameter("@usuario", user)}))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        usuario = UserAdapter.Current.adapt(vs);
                    }
                }
                string descripcion = "Se obtuvo la informacion del usuario con user: " + user;
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return usuario;

        }

        public Usuario GetUser(string user)
        {
            Usuario usuario = new Usuario();
            try
            {
                string sql = "SELECT * FROM [dbo].[Usuario] WHERE id_usuario = @usuario";
                using (var dr = SqlHelper.ExecuteReader(sql, System.Data.CommandType.Text, new SqlParameter[] {
                                                       new SqlParameter("@usuario", Guid.Parse(user))}))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        usuario = UserAdapter.Current.adapt(vs);
                    }
                }
                string descripcion = "Se obtuvo la informacion del usuario con id " + user;
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return usuario;

        }

        public Usuario LoginUserBo(string user, string password)
        {
            Usuario usuario = new Usuario();
            try
            {
                string statement = "SELECT * FROM [dbo].[Usuario] WHERE usuario = @usuario and contraseña = @contraseña";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[] {
                                                       new SqlParameter("@Usuario", user),
                                                       new SqlParameter("@contraseña", password)}))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        usuario = UserAdapter.Current.adapt(vs);
                    }
                }
                string descripcion = "Login del usuario " + user;
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return usuario;

        }

        public Usuario GetOne(string[] criterios, string[] valores)
        {
            throw new NotImplementedException();
        }

        public void delete(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public void Add(Usuario entity)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(InsertStatement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@id_usuario", Guid.Parse(entity.id)),
                    new SqlParameter("@usuario", entity.user),
                    new SqlParameter("@nombre", entity.name),
                    new SqlParameter("@contraseña", entity.password),
                    new SqlParameter("@apellido", entity.lastname),
                    new SqlParameter("@tipo_usuario", int.Parse(entity.user_type)),
                    new SqlParameter("@cuit_empresa", entity.cuit_empresa),
                    new SqlParameter("@fecha_creacion", entity.fecha_creacion),
                    new SqlParameter("@fecha_modificacion", entity.fecha_modificacion)
                });
                string descripcion = "Se genero el usuario" + entity.user;
                BitacoraService.Current.AddBitacora("INFO", descripcion, entity.id);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.ToString(), entity.id);
            }
        }

        public IEnumerable<Usuario> GetAll(string cuit_empresa)
        {
            List<Usuario> usuarioList = new List<Usuario>();
            try
            {
                Usuario usuario = new Usuario();
                string statement = "SELECT * FROM [dbo].[Usuario] WHERE cuit_empresa = @cuit_empresa and tipo_usuario = 1";
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[] {
                                                       new SqlParameter("@cuit_empresa", cuit_empresa)}))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        usuario = UserAdapter.Current.adapt(vs);
                        usuarioList.Add(usuario);
                    }
                }
                string descripcion = "Se realizo un Get de todos los usuario de la empresa con cuit" + cuit_empresa.ToString();
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return usuarioList;
        }

        public IEnumerable<Usuario> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Usuario entity)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(UpdateStatement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@id_usuario", Guid.Parse(entity.id)),
                    new SqlParameter("@usuario", entity.user),
                    new SqlParameter("@nombre", entity.name),
                    new SqlParameter("@contraseña", entity.password),
                    new SqlParameter("@apellido", entity.lastname),
                    new SqlParameter("@tipo_usuario", Convert.ToInt32(entity.rol.Id)),
                    new SqlParameter("@cuit_empresa", entity.cuit_empresa),
                    new SqlParameter("@fecha_modificacion", entity.fecha_modificacion)
                });
                string descripcion = "Se genero un update del usuario" + entity.user;
                BitacoraService.Current.AddBitacora("INFO", descripcion, entity.id);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.ToString(), entity.id);
            }
        }
    }

}