﻿using DAL.Interface;
using DAL.Manager.Adapters;
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
            Empresa empresa = new Empresa();
            try
            {
                using (var dr = SqlHelper.ExecuteReader(SelectOneStatement, System.Data.CommandType.Text,
                    new SqlParameter[]
                    {
                        new SqlParameter("@cuit_empresa", valores.First())
                    }))
                {
                    if (dr.Read())
                    {
                        object[] values = new object[dr.FieldCount];
                        dr.GetValues(values);
                        empresa = EmpresaAdapter.Current.adapt(values);
                    }
                }
                string descripcion = "Se obtuvo la informacion de la empresa con cuit:" + valores.First();
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return empresa;
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
                    new SqlParameter("@cuit_empresa", entity.cuit),
                    new SqlParameter("@nombre_empresa", entity.name),
                    new SqlParameter("@direccion_empresa", entity.direccion),
                    new SqlParameter("@fecha_creacion", entity.fecha_creacion),
                    new SqlParameter("@fecha_modificacion", entity.fecha_modificacion)
                });

                string descripcion = "Se realizo el alta de la empresa con cuit:" + entity.cuit;
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
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
                string descripcion = "Se obtuvo una lista de todas las empresas";
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return list;
        }

        public IEnumerable<Empresa> GetAllEmpresasAsociadasMedico(string userid)
        {
            List<Empresa> list = new List<Empresa>();
            Empresa empresa = new Empresa();
            string sql = "Select e.id_empresa, e.cuit_empresa, e.nombre_empresa, e.direccion_empresa, e.fecha_creacion, e.fecha_modificacion from Empresa as e join medico_empresa as me on me.id_empresa = e.id_empresa where me.id_usuario_medico = @usuario";
            try
            {
                using (var dr = SqlHelper.ExecuteReader(sql, System.Data.CommandType.Text, new SqlParameter[]
                {
                     new SqlParameter("@usuario", Guid.Parse(userid)),
                }))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        empresa = EmpresaAdapter.Current.adapt(vs);
                        list.Add(empresa);
                    }
                }
                string descripcion = "Se obtuvo una lista de todas las empresas relacionadas con el medico con id:" + userid;
                BitacoraService.Current.AddBitacora("INFO", descripcion, "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return list;
        }

        public void Update(Empresa entity)
        {
            throw new NotImplementedException();
        }
    }

}
