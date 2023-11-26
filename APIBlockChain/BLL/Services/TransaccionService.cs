﻿using DAL.Managers;
using DAL.Tools.Service;
using DOMAIN.DomainDal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public sealed class TransaccionService
	{
        #region singleton
        private readonly static TransaccionService _instance = new TransaccionService();

		public static TransaccionService Current
		{
			get
			{
				return _instance;
			}
		}

		private TransaccionService()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

		public Transaccion getOne(string TrxTransaccion)
		{
			Transaccion transaccion = new Transaccion();
			try
			{
				transaccion = TransaccionManager.Current.getTransaccion(TrxTransaccion);
			}
			catch (Exception ex)
			{
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
			return transaccion;
		}
		public IEnumerable<Transaccion> getTransaccionesUser(string user)
		{
			List<Transaccion> transaccions = new List<Transaccion>();
			try
			{
				transaccions = (List<Transaccion>)TransaccionManager.Current.getListTransaccionesUser(user);
			}
			catch (Exception ex)
			{
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
			return transaccions;
		}
		public IEnumerable<Transaccion> getTransaccionesList() 
		{
			List<Transaccion> transaccions = new List<Transaccion>();
			try
			{
				transaccions = (List<Transaccion>)TransaccionManager.Current.getListTransacciones();
			}
			catch (Exception ex)
			{
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
			return transaccions;
		}

        public IEnumerable<Transaccion> getTransaccionesFechas(string fechaInicio, string fechaFin)
        {
            List<Transaccion> transaccions = new List<Transaccion>();
            try
            {
                transaccions = (List<Transaccion>)TransaccionManager.Current.getListTransaccionesFecha(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return transaccions;
        }

        public IEnumerable<Transaccion> getTransaccionesFechasUser(string fechaInicio, string fechaFin, string user)
        {
            List<Transaccion> transaccions = new List<Transaccion>();
            try
            {
               
                transaccions = (List<Transaccion>)TransaccionManager.Current.getListTransaccionesFechaUser(fechaInicio, fechaFin, user);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return transaccions;
        }

        public IEnumerable<Transaccion> getTransaccionesFechasCompany(string fechaInicio, string fechaFin, string user)
        {
            List<Transaccion> transaccions = new List<Transaccion>();
            try
            {
                WalletCompany walletCompany = new WalletCompany();
                walletCompany = BilleteraManager.Current.GetWalletCompany(user);
                transaccions = (List<Transaccion>)TransaccionManager.Current.getListTransaccionesFechaCompany(fechaInicio, fechaFin, walletCompany.wallet);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return transaccions;
        }

        private string GetNombreUser(string guid)
        {
            string user = "";
            using (var clientHandler = new HttpClientHandler())
            {
                string url = "https://localhost:7151/api/User/GetUser/" + guid;
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);
                client.DefaultRequestHeaders.Clear();
                var response = client.GetAsync(url).Result;
                var res = response.Content.ReadAsStringAsync().Result;
                dynamic r = JObject.Parse(res);
                user = Convert.ToString(r["name"]);
            }
            return user;
        }
    }

}