using System.Threading.Tasks;
using proyecto.API.Models;

namespace proyecto.API.Data
{
    public interface IRepositorioAuth
    {
         Task<Usuario> Registrar(Usuario usuario, string password);

         Task<Usuario> Login (string nombreUsuario, string password);

         Task<bool> ExisteUsuario(string nombreUsuario);


    }
}