using DAL.Interface;
using DAL.Manager.Adapter;
using DAL.Tools;
using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{

    public sealed class SolicitudManager : IGenericRepository<Solicitud>
    {
        #region singleton
        private readonly static SolicitudManager _instance = new SolicitudManager();

        public static SolicitudManager Current
        {
            get
            {
                return _instance;
            }
        }

        private SolicitudManager()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[solicitudes] (id_solicitud, cuit_empresa, id_usuario, id_tipo_solicitud, Descripcion, Estado, fecha_creacion, fecha_modificacion) VALUES (@id_solicitud, @cuit_empresa, @id_usuario, @id_tipo_solicitud, @Descripcion, @Estado, @fecha_creacion, @fecha_modificacion)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[solicitudes] SET id_tipo_solicitud = @id_tipo_solicitud, Descripcion = @Descripcion, Estado = @Estado, fecha_modificacion = @fecha_modificacion WHERE cuit_empresa = @cuit_empresa and id_usuario = @id_usuario";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[solicitudes] WHERE cuit_empresa = @cuit_empresa and id_usuario = @id_usuario";
        }

        private string SelectOneStatement
        {
            get => "SELECT * FROM [dbo].[solicitudes] WHERE cuit_empresa = @cuit_empresa and id_usuario = @id_usuario";
        }

        private string SelectAllStatement
        {
            get => "SELECT * FROM [dbo].[solicitudes] WHERE cuit_empresa = @cuit_empresa and id_tipo_solicitud = @id_tipo_solicitud";
        }
        #endregion
        public Solicitud GetOne(string[] criterios, string[] valores)
        {
            try
            {
                string where = "";
                for(int c = 0; c < criterios.Length; c++)
                {
                    where = where + (c == 0 ? "" : " AND ") + criterios[c] + "= '" + valores[c] + "'";
                }
                string query = "SELECT * FROM [dbo].[solicitudes] WHERE " + where;
                using (var dr = SqlHelper.ExecuteReader(query, System.Data.CommandType.Text))
                {
                    if (dr.Read())
                    {
                        object[] values = new object[dr.FieldCount];
                        dr.GetValues(values);
                        return SolicitudAdapter.Current.adapt(values);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            throw new NotImplementedException();
        }

        public void delete(Solicitud entity)
        {
            throw new NotImplementedException();
        }

        public void Add(Solicitud entity)
        {
            try
            {
                
                SqlHelper.ExecuteNonQuery(InsertStatement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@id_solicitud", Guid.Parse(entity.id_solicitud)),
                    new SqlParameter("@cuit_empresa", Convert.ToInt32(entity.cuit_empresa)),
                    new SqlParameter("@id_usuario", Guid.Parse(entity.id_usuario)),
                    new SqlParameter("@id_tipo_solicitud", Convert.ToInt32(entity.tipo_Solicitud.id)),
                    new SqlParameter("@Descripcion", entity.Descripcion),
                    new SqlParameter("@Estado", Convert.ToInt32(entity.estado)),
                    new SqlParameter("@fecha_creacion", entity.fecha_creacion),
                    new SqlParameter("@fecha_modificacion", entity.fecha_modificacion)
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Solicitud> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Solicitud> GetAllSolicitudes(int id_tipo_solicitud, int cuit_empresa)
        {
            List<Solicitud> list = new List<Solicitud>();
            Solicitud solicitud = new Solicitud();
            try
            {
                using (var dr = SqlHelper.ExecuteReader(SelectAllStatement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@id_tipo_solicitud", id_tipo_solicitud),
                    new SqlParameter("@cuit_empresa", cuit_empresa),
                }))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        solicitud = SolicitudAdapter.Current.adapt(vs);
                        list.Add(solicitud);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        public IEnumerable<Solicitud> GetAllSolicitudesUser(string id_user)
        {
            List<Solicitud> list = new List<Solicitud>();
            Solicitud solicitud = new Solicitud();
            string statement = "SELECT * FROM [dbo].[solicitudes] WHERE id_usuario = @id_usuario";
            try
            {
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@id_usuario", Guid.Parse(id_user))
                }))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        solicitud = SolicitudAdapter.Current.adapt(vs);
                        list.Add(solicitud);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        

        public void Update(Solicitud entity)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(UpdateStatement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@cuit_empresa", entity.cuit_empresa),
                    new SqlParameter("@id_usuario", entity.id_usuario),
                    new SqlParameter("@id_tipo_solicitud", entity.tipo_Solicitud.id),
                    new SqlParameter("@Descripcion", entity.Descripcion),
                    new SqlParameter("@Estado", Convert.ToInt32(entity.estado)),
                    new SqlParameter("@fecha_modificacion", entity.fecha_modificacion)
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
