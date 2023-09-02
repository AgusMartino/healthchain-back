using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DOMAIN
{
    public class Empresa
    {
        
        
        [Required]
        public string cuit { get; set; }
        public string id_empresa { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string direccion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_modificacion { get; set; }
    }
}
