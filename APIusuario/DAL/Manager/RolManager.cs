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
    public sealed class RolManager : IGenericRepository<Rol>
    {
        #region singleton
        private readonly static RolManager _instance = new RolManager();

        public static RolManager Current
        {
            get
            {
                return _instance;
            }
        }

        private RolManager()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Rol] (Id_Rol, Rol) VALUES (@Id_Rol, @Rol)";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Rol] WHERE  Id_Rol = @Id_Rol";
        }

        private string SelectOneStatement
        {
            get => "SELECT * FROM [dbo].[Rol] WHERE Id_Rol = @Id_Rol";
        }

        private string SelectAllStatement
        {
            get => "SELECT * FROM [dbo].[Rol]";
        }
        #endregion

        public Rol GetOne(string[] criterios, string[] valores)
        {
            try
            {
                Rol rol = new Rol();
                using (var dr = SqlHelper.ExecuteReader(SelectOneStatement, System.Data.CommandType.Text, 
                    new SqlParameter[]
                    { 
                        new SqlParameter("@Id_Rol", int.Parse(valores.First()))
                    }))
                {
                    if (dr.Read())
                    {
                        object[] values = new object[dr.FieldCount];
                        dr.GetValues(values);
                        rol = RolAdapter.Current.adapt(values);
                    }
                }
                return rol;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public void delete(Rol entity)
        {
            throw new NotImplementedException();
        }

        public void Add(Rol entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rol> GetAll()
        {
            List<Rol> list = new List<Rol>();
            Rol rol = new Rol();
            try
            {
                using(var dr = SqlHelper.ExecuteReader(SelectAllStatement, System.Data.CommandType.Text))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        rol = RolAdapter.Current.adapt(vs);
                        list.Add(rol);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return list;
        }

        public void Update(Rol entity)
        {
            throw new NotImplementedException();
        }
        
    }

}
