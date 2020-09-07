using System.ComponentModel.DataAnnotations;

namespace proyecto.API.Dtos
{
    public class UsuarioARegistrarDto
    {
        [Required]
        public string nombreUsuario { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4 , ErrorMessage = "El password tiene que ser entre 4 y 8 caracteres" )]
        public string password { get; set; }
    }
}