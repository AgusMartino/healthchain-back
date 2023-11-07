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
            get => "INSERT INTO [dbo].[solicitudes] (id_solicitud, cuit_empresa, id_usuario, id_tipo_solicitud, Descripcion, Estado, fecha_creacion, fecha_modificacion, id_Rol_solicitado) VALUES (@id_solicitud, @cuit_empresa, @id_usuario, @id_tipo_solicitud, @Descripcion, @Estado, @fecha_creacion, @fecha_modificacion, @id_Rol_solicitado)";
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
            Solicitud solicitud = new Solicitud();
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
                        solicitud = SolicitudAdapter.Current.adapt(values);
                    }
                }
                string descripcion = "Se obtuvo la solicitud con los criterios:" + where;
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return solicitud;
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
                    new SqlParameter("@cuit_empresa", entity.cuit_empresa),
                    new SqlParameter("@id_usuario", Guid.Parse(entity.id_usuario)),
                    new SqlParameter("@id_tipo_solicitud", Convert.ToInt32(entity.tipo_Solicitud.id)),
                    new SqlParameter("@Descripcion", entity.Descripcion),
                    new SqlParameter("@Estado", Convert.ToInt32(entity.estado)),
                    new SqlParameter("@fecha_creacion", entity.fecha_creacion),
                    new SqlParameter("@fecha_modificacion", entity.fecha_modificacion),
                    new SqlParameter("@id_Rol_solicitado", entity.Rolseleccionado)
                });
                string descripcion = "Se creo la solicitud con id:" + entity.id_solicitud;
                BitacoraService.Current.AddBitacora("INFO", descripcion, entity.id_usuario);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.ToString(), entity.id_usuario);
            }
        }

        public IEnumerable<Solicitud> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Solicitud> GetAllSolicitudes(int id_tipo_solicitud, string cuit_empresa)
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
                string descripcion = "Se Obtuvo el listado de solicitudes de la empresa con cuit:" + cuit_empresa + "y con el tipo de solicitud id:" + id_tipo_solicitud.ToString();
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
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
                string descripcion = "Se obtuvo una lista de las solicitudes realizadas por el usuario con id:" + id_user;
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
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
                    new SqlParameter("@id_usuario", Guid.Parse(entity.id_usuario)),
                    new SqlParameter("@id_tipo_solicitud", entity.tipo_Solicitud.id),
                    new SqlParameter("@Descripcion", entity.Descripcion),
                    new SqlParameter("@Estado", Convert.ToInt32(entity.estado)),
                    new SqlParameter("@fecha_modificacion", entity.fecha_modificacion)
                });

                string descripcion = "Se actualizo la informacion de la solicitud con id:" + entity.id_solicitud;
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }

        public string GetEstado(string id)
        {
            string estado = "";
            string statement = "SELECT estado FROM [dbo].[Estado] WHERE id_estado = @id";
            try
            {
                using (var dr = SqlHelper.ExecuteReader(statement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@id", int.Parse(id))
                }))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        object v = vs.First();
                        estado = v.ToString();
                    }
                }
                string descripcion = "Se obtuvo el estado con id:" + id;
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return estado;

        }
    }

}
