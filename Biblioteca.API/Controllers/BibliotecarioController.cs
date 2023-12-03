using Biblioteca.API.Models;
using Biblioteca.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class BibliotecarioController : ControllerBase
    {
        [HttpGet]
        [Route("/Bibliotecario")]
        public IActionResult Get(
            [FromServices] AppDbContext context) => 
                Ok( context.Bibliotecario!.ToList());

        
        [HttpGet("/Bibliotecario/Details/{id:int}")]
        public IActionResult GetById([FromRoute] int id,
            [FromServices] AppDbContext context)
        {
            var bibliotecarioModel = context.Bibliotecario!.FirstOrDefault(x => x.BibliotecarioID == id);
            if (bibliotecarioModel == null) {
                return NotFound();
            }

            return Ok(bibliotecarioModel);
        }

        [HttpPost("/Bibliotecario/Create")]
        public IActionResult Post([FromBody] BibliotecarioModel bibliotecarioModel,
            [FromServices] AppDbContext context)
        {
            context.Bibliotecario!.Add(bibliotecarioModel);
            context.SaveChanges();
            return Created($"/{bibliotecarioModel.BibliotecarioID}", bibliotecarioModel);
        }

        [HttpPut("/Bibliotecario/Edit/{id:int}")]
        public IActionResult Put([FromRoute] int id, 
            [FromBody] BibliotecarioModel bibliotecarioModel,
            [FromServices] AppDbContext context)
        {
            var model = context.Bibliotecario!.FirstOrDefault(x => x.BibliotecarioID == id);
            if (model == null) {
                return NotFound();
            }

            model.Nome = bibliotecarioModel.Nome;
            model.Email = bibliotecarioModel.Email;
            model.Senha = bibliotecarioModel.Senha;
            model.Telefone = bibliotecarioModel.Telefone;

            context.Bibliotecario!.Update(model);
            context.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("/Bibliotecario/Delete/{id:int}")]
        public IActionResult Delete([FromRoute] int id, 
            [FromServices] AppDbContext context)
        {
            var model = context.Bibliotecario!.FirstOrDefault(x => x.BibliotecarioID == id);
            if (model == null) {
                return NotFound();
            }

            context.Bibliotecario!.Remove(model);
            context.SaveChanges();
            return Ok(model);
        }
    }
}