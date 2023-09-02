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

    public sealed class Tipo_solicitudManager : IGenericRepository<tipo_solicitud>
    {
        #region singleton
        private readonly static Tipo_solicitudManager _instance = new Tipo_solicitudManager();

        public static Tipo_solicitudManager Current
        {
            get
            {
                return _instance;
            }
        }

        private Tipo_solicitudManager()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        #region Statements
        private string SelectOneStatement
        {
            get => "SELECT * FROM [dbo].[tipo_solicitud] WHERE id_tipo_solicitud = @id_tipo_solicitud";
        }

        private string SelectAllStatement
        {
            get => "SELECT * FROM [dbo].[tipo_solicitud]";
        }
        #endregion
        public tipo_solicitud GetOne(string[] criterios, string[] valores)
        {
            try
            {
                tipo_solicitud solicitud = new tipo_solicitud();
                using (var dr = SqlHelper.ExecuteReader(SelectOneStatement, System.Data.CommandType.Text,
                   new SqlParameter[] { new SqlParameter("@id_tipo_solicitud", int.Parse(valores.First())) }))
                {
                    if (dr.Read())
                    {
                        object[] values = new object[dr.FieldCount];
                        dr.GetValues(values);

                        solicitud = Tipo_SolicitudaAdapter.Current.adapt(values);
                    }
                    return solicitud;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void delete(tipo_solicitud entity)
        {
            throw new NotImplementedException();
        }

        public void Add(tipo_solicitud entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tipo_solicitud> GetAll()
        {
            List<tipo_solicitud> list = new List<tipo_solicitud>();
            tipo_solicitud solicitud = new tipo_solicitud();
            try
            {
                using (var dr = SqlHelper.ExecuteReader(SelectAllStatement, System.Data.CommandType.Text))
                {
                    while (dr.Read())
                    {
                        object[] vs = new object[dr.FieldCount];
                        dr.GetValues(vs);
                        solicitud = Tipo_SolicitudaAdapter.Current.adapt(vs);
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

        public void Update(tipo_solicitud entity)
        {
            throw new NotImplementedException();
        }
        //Implent here the initialization of your singleton
    }
}
