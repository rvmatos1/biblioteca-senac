using Biblioteca.API.Models;
using Biblioteca.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        [HttpGet]
        [Route("/Livro")]
        public IActionResult Get(
            [FromServices] AppDbContext context) => 
                Ok( context.Livro!.ToList());

        
        [HttpGet("/Livro/Details/{id:int}")]
        public IActionResult GetById([FromRoute] int id,
            [FromServices] AppDbContext context)
        {
            var livroModel = context.Livro!.FirstOrDefault(x => x.LivroID == id);
            if (livroModel == null) {
                return NotFound();
            }

            return Ok(livroModel);
        }

        [HttpPost("/Livro/Create")]
        public IActionResult Post([FromBody] LivroModel livroModel,
            [FromServices] AppDbContext context)
        {
            context.Livro!.Add(livroModel);
            context.SaveChanges();
            return Created($"/{livroModel.LivroID}", livroModel);
        }

        [HttpPut("/Livro/Edit/{id:int}")]
        public IActionResult Put([FromRoute] int id, 
            [FromBody] LivroModel livroModel,
            [FromServices] AppDbContext context)
        {
            var model = context.Livro!.FirstOrDefault(x => x.LivroID == id);
            if (model == null) {
                return NotFound();
            }

            model.Autor = livroModel.Autor;
            model.TituloLivro = livroModel.TituloLivro;
            model.ISBN = livroModel.ISBN;
            model.AnoPublicacao = livroModel.AnoPublicacao;
            model.QuantidadeDisponivel = livroModel.QuantidadeDisponivel;

            context.Livro!.Update(model);
            context.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("/Livro/Delete/{id:int}")]
        public IActionResult Delete([FromRoute] int id, 
            [FromServices] AppDbContext context)
        {
            var model = context.Livro!.FirstOrDefault(x => x.LivroID == id);
            if (model == null) {
                return NotFound();
            }

            context.Livro!.Remove(model);
            context.SaveChanges();
            return Ok(model);
        }
    }
}