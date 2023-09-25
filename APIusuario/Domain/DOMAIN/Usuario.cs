namespace Domain.DOMAIN
{
    public class Usuario
    {
        public string id { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string user_type { get; set; }
        public string cuit_empresa { get; set; }
        public Rol rol { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_modificacion { get; set; }

    }
}
