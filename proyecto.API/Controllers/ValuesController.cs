using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyecto.API.Data;

namespace proyecto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Contexto _contexto;

        public ValuesController(Contexto contexto)
        {
            _contexto = contexto;

        }

        
        [HttpGet]
        public async Task<IActionResult> GetValores()
        {
            var valores= await _contexto.Valores.ToListAsync();

            return Ok(valores);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValor(int id)
        {
            var valor=await _contexto.Valores.FirstOrDefaultAsync(x => x.id == id);

            return Ok(valor);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
