using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using proyecto.API.Data;
using proyecto.API.Dtos;
using proyecto.API.Models;

namespace proyecto.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepositorioAuth _repo;
        private readonly IConfiguration _config;

        public AuthController(IRepositorioAuth repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;

        }


        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(UsuarioARegistrarDto usuarioARegistarDto)
        {

            //falta validacion

            usuarioARegistarDto.nombreUsuario = usuarioARegistarDto.nombreUsuario.ToLower();

            if (await _repo.ExisteUsuario(usuarioARegistarDto.nombreUsuario))
                return BadRequest("Ya existe ese nombre de usuario");

            var usuarioACrear = new Usuario
            {
                nombreUsuario = usuarioARegistarDto.nombreUsuario
            };

            var usuarioCreado = _repo.Registrar(usuarioACrear, usuarioARegistarDto.password);

            return StatusCode(201);
        }


        [HttpPost("login")]

        public async Task<IActionResult> Login(UsuarioParaLoginDto usuarioParaLoginDto)
        {

            var usuarioDB = await _repo.Login(usuarioParaLoginDto.nombreUsuario.ToLower(), usuarioParaLoginDto.password);

            if (usuarioDB == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioDB.id.ToString()),
                new Claim(ClaimTypes.Name, usuarioDB.nombreUsuario)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var descriptorToken = new SecurityTokenDescriptor 
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(descriptorToken);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });    

        }
    }
}