using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto.API.Models;

namespace proyecto.API.Data
{
    public class RepositorioAuth : IRepositorioAuth
    {
        private readonly Contexto _contexto;

        public RepositorioAuth(Contexto contexto)
        {
            _contexto = contexto;

        }

        public async Task<bool> ExisteUsuario(string nombreUsuario)
        {
            if (await _contexto.Usuarios.AnyAsync(x => x.nombreUsuario == nombreUsuario))
                return true;

            return false;    
        }

        public async Task<Usuario> Login(string nombreUsuario, string password)
        {
            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(x => x.nombreUsuario == nombreUsuario );

            if (usuario == null)
                return null;

            if (!VerificarPasswordHash(password, usuario.passwordHash,usuario.passwordSalt))
                return null;

            return usuario;
        }

        private bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                
                var hash= hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                
                for (int i = 0 ; i < hash.Length; i++)
                {

                    if (hash[i]!=passwordHash[i])
                        return false;
                        
                }
                
            }
            return true;
        }

        public async Task<Usuario> Registrar(Usuario usuario, string password)
        {
            byte[] passwordHash, passwordSalt;
            CrearPasswordHash(password,out passwordHash,out passwordSalt);

            usuario.passwordHash= passwordHash;
            usuario.passwordSalt=passwordSalt;

            await _contexto.Usuarios.AddAsync(usuario);
            await _contexto.SaveChangesAsync();

            return usuario;


        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt= hmac.Key;
                passwordHash= hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }
    }
}