using DAL.Interface;
using DAL.Manager.Adapter;
using DAL.Tools;
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
            try
            {
                Usuario usuario = new Usuario();
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
                return usuario;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public Usuario LoginUserBo(string user, string password)
        {
            try
            {
                Usuario usuario = new Usuario();
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
                return usuario;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
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
                    new SqlParameter("@id_usuario", Guid.Parse(entity.Id)),
                    new SqlParameter("@usuario", entity.user),
                    new SqlParameter("@nombre", entity.name),
                    new SqlParameter("@contraseña", entity.password),
                    new SqlParameter("@apellido", entity.lastname),
                    new SqlParameter("@tipo_usuario", Convert.ToInt32(entity.rol.Id)),
                    new SqlParameter("@cuit_empresa", Convert.ToInt32(entity.cuit_empresa)),
                    new SqlParameter("@fecha_creacion", entity.fecha_creacion),
                    new SqlParameter("@fecha_modificacion", entity.fecha_modificacion)
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Usuario> GetAll(int cuit_empresa)
        {

            try
            {
                Usuario usuario = new Usuario();
                List<Usuario> usuarioList = new List<Usuario>();
                string statement = "SELECT * FROM [dbo].[Usuario] WHERE cuit_empresa = @cuit_empresa";
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
                return usuarioList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
                    new SqlParameter("@id_usuario", Guid.Parse(entity.Id)),
                    new SqlParameter("@usuario", entity.user),
                    new SqlParameter("@nombre", entity.name),
                    new SqlParameter("@contraseña", entity.password),
                    new SqlParameter("@apellido", entity.lastname),
                    new SqlParameter("@tipo_usuario", Convert.ToInt32(entity.rol.Id)),
                    new SqlParameter("@cuit_empresa", Convert.ToInt32(entity.cuit_empresa)),
                    new SqlParameter("@fecha_modificacion", entity.fecha_modificacion)
                });
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

}