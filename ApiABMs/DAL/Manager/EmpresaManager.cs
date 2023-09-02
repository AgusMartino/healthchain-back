using DAL.Interface;
using DAL.Manager.Adapters;
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
    public sealed class EmpresaManager : IGenericRepository<Empresa>
    {
        #region singleton
        private readonly static EmpresaManager _instance = new EmpresaManager();

        public static EmpresaManager Current
        {
            get
            {
                return _instance;
            }
        }

        private EmpresaManager()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Empresa] (id_empresa, cuit_empresa, nombre_empresa, direccion_empresa, fecha_creacion, fecha_modificacion) VALUES (@id_empresa, @cuit_empresa, @nombre_empresa, @direccion_empresa, @fecha_creacion, @fecha_modificacion)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Empresa] SET nombre_empresa = @nombre_empresa, direccion_empresa = @direccion_empresa, fecha_modificacion = @fecha_modificacion WHERE id_empresa = @id_empresa";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Empresa] WHERE  cuit_empresa = @cuit_empresa";
        }

        private string SelectOneStatement
        {
            get => "SELECT * FROM [dbo].[Empresa] WHERE cuit_empresa = @cuit_empresa";
        }

        private string SelectAllStatement
        {
            get => "SELECT * FROM [dbo].[Empresa]";
        }
        #endregion
        public Empresa GetOne(string[] criterios, string[] valores)
        {
            try
            {
                Empresa empresa = new Empresa();
                using (var dr = SqlHelper.ExecuteReader(SelectOneStatement, System.Data.CommandType.Text,
                    new SqlParameter[]
                    {
                        new SqlParameter("@cuit_empresa", Convert.ToInt32(valores.First()))
                    }))
                {
                    if (dr.Read())
                    {
                        object[] values = new object[dr.FieldCount];
                        dr.GetValues(values);
                        empresa = EmpresaAdapter.Current.adapt(values);
                    }
                }
                return empresa;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void delete(Empresa entity)
        {
            throw new NotImplementedException();
        }

        public void Add(Empresa entity)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(InsertStatement, System.Data.CommandType.Text, new SqlParameter[]
                {
                    new SqlParameter("@id_empresa", Guid.Parse(entity.id_empresa)),
                    new SqlParameter("@cuit_empresa", Convert.ToInt32(entity.cuit)),
                    new SqlParameter("@nombre_empresa", entity.name),
                    new SqlParameter("@direccion_empresa", entity.direccion),
                    new SqlParameter("@fecha_creacion", entity.fecha_creacion),
                    new SqlParameter("@fecha_modificacion", entity.fecha_modificacion)
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Empresa> GetAll()
        {
            List<Empresa> list = new List<Empresa>();
            Empresa empresa = new Empresa();
            try
            {
                using (var dr = SqlHelper.ExecuteReader(SelectAllStatement, System.Data.CommandType.Text))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        empresa = EmpresaAdapter.Current.adapt(vs);
                        list.Add(empresa);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return list;
        }

        public void Update(Empresa entity)
        {
            throw new NotImplementedException();
        }
    }

}
