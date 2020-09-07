namespace proyecto.API.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string nombreUsuario { get; set; }

        public byte[] passwordHash { get; set; }

        public byte[] passwordSalt { get; set; }
    }
}