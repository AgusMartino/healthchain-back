using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public sealed class NFTService
    {
        #region singleton
        private readonly static NFTService _instance = new NFTService();

		public static NFTService Current
		{
			get
			{
				return _instance;
			}
		}

		private NFTService()
		{
			//Implent here the initialization of your singleton
		}
        #endregion
    }

}
