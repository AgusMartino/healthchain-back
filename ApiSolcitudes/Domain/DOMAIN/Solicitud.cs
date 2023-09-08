using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DOMAIN
{
    public class Solicitud
    {
        public string id_solicitud { get; set; }
        public string cuit_empresa { get; set; }
        public string id_usuario { get; set; }
        public string Rolseleccionado { get; set; }
        public tipo_solicitud tipo_Solicitud { get; set; }
        public string Descripcion { get; set; }
        public string estado { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_modificacion { get; set; }
        public string user { get; set; }
        public string nombreEmpresa { get; set; }


    }
}
