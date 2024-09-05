using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AMMA20230901.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private static List<Nota> notas = new List<Nota>();

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Nota> ObtenerNotas()
        {
            // Devuelve todas las notas, ya que este endpoint es público
            return notas;
        }

        [HttpPost]
        [Authorize]
        public IActionResult RegistrarNota(Nota nota)
        {
            // Solo usuarios autenticados pueden registrar nota
            notas.Add(nota);
            return CreatedAtAction(nameof(ObtenerNota), new { id = nota.Id }, nota);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<Nota> ObtenerNota(int id)
        {
            var nota = notas.FirstOrDefault(n => n.Id == id);
            if (nota == null)
            {
                return NotFound();
            }
            return Ok(nota);
        }

        public class Nota
        {
            public int Id { get; set; }
            public string Titulo { get; set; }
            public string Contenido { get; set; }
        }
    }
}
