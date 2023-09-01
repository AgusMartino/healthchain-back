namespace Domain.DOMAIN
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public int user_type { get; set; }
        public int rol { get; set; }

    }
}
