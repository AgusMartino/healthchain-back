﻿using DOMAIN.DomainDal;
using DOMAIN.DomainRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Adapter
{
	public sealed class NFTAdapter
	{
        #region singleton
        private readonly static NFTAdapter _instance = new NFTAdapter();

		public static NFTAdapter Current
		{
			get
			{
				return _instance;
			}
		}

		private NFTAdapter()
		{
			//Implent here the initialization of your singleton
		}
        #endregion

        public NftRequest adapt(object[] values)
        {
            NftRequest nft = new NftRequest
            {
                TokenNFTid = values[(int)Columns.TokenNFTid].ToString(),
                Nombre_paciente = values[(int)Columns.Nombre_paciente].ToString(),
                Apellido_paciente = values[(int)Columns.Apellido_paciente].ToString(),
                Dni = values[(int)Columns.Dni].ToString(),
                Cobertura = values[(int)Columns.Cobertura].ToString(),
                Consulta = values[(int)Columns.Consulta].ToString(),
                Patologia = values[(int)Columns.Patologia].ToString(),
                Estado = values[(int)Columns.estado].ToString(),
                precio = values[(int)Columns.precio].ToString()
            };
            return nft;
        }

        public NftRequest adaptGetNFT(object[] values)
        {
            NftRequest nft = new NftRequest
            {
                id_user = values[(int)ColumnsGetNFT.id_user].ToString(),
                TokenNFTid = values[(int)ColumnsGetNFT.TokenNFTid].ToString(),
                Nombre_paciente = values[(int)ColumnsGetNFT.Nombre_paciente].ToString(),
                Apellido_paciente = values[(int)ColumnsGetNFT.Apellido_paciente].ToString(),
                Dni = values[(int)ColumnsGetNFT.Dni].ToString(),
                Cobertura = values[(int)ColumnsGetNFT.Cobertura].ToString(),
                Consulta = values[(int)ColumnsGetNFT.Consulta].ToString(),
                Patologia = values[(int)ColumnsGetNFT.Patologia].ToString(),
                Estado = values[(int)ColumnsGetNFT.estado].ToString(),
                precio = values[(int)ColumnsGetNFT.precio].ToString()
            };
            return nft;
        }
        private enum ColumnsGetNFT
        {
            id_user,
            TokenNFTid,
            Nombre_paciente,
            Apellido_paciente,
            Dni,
            Cobertura,
            Consulta,
            Patologia,
            estado,
            precio
        }

        private enum Columns
        {
            TokenNFTid,
            Nombre_paciente,
            Apellido_paciente,
            Dni,
            Cobertura,
            Consulta,
            Patologia,
            estado,
            precio
        }
    }

}
