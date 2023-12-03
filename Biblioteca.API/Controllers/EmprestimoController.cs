using Biblioteca.API.Models;
using Biblioteca.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmprestimoController : ControllerBase
    {
        [HttpGet]
        [Route("/Emprestimo")]
        public IActionResult Get([FromServices] AppDbContext context){
            var emprestimos = context.Emprestimo!.Include(b => b.Bibliotecario).Include(l => l.Livro).ToList();
            return Ok(emprestimos);
        }

        [HttpGet("/Emprestimo/Details/{id:int}")]
        public IActionResult GetById([FromRoute] int id, [FromServices] AppDbContext context)
        {
            var emprestimoModel = context.Emprestimo!.Include(b => b.Bibliotecario).Include(l => l.Livro).FirstOrDefault(x => x.EmprestimoID == id);
            if (emprestimoModel == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                ID = emprestimoModel.EmprestimoID,
                DataEmprestimo = emprestimoModel.DataEmprestimo,
                DataDevolucaoPrevista = emprestimoModel.DataDevolucaoPrevista,
                Bibliotecario = new
                {
                    ID = emprestimoModel.Bibliotecario!.BibliotecarioID,
                    Nome = emprestimoModel.Bibliotecario.Nome,
                    Email = emprestimoModel.Bibliotecario.Email,
                    Telefone = emprestimoModel.Bibliotecario.Telefone
                },                
                Livro = new
                {
                    ID = emprestimoModel.Livro!.LivroID,
                    Titulo = emprestimoModel.Livro.TituloLivro,
                    Autor = emprestimoModel.Livro.Autor,
                    ISBN = emprestimoModel.Livro.ISBN,
                    AnoPublicacao = emprestimoModel.Livro.AnoPublicacao,
                    QuantidadeDisponivel = emprestimoModel.Livro.QuantidadeDisponivel
                },
                NomeUsuario = emprestimoModel.NomeUsuario
            });
        }

        [HttpPost("/Emprestimo/Create")]
        public IActionResult Post([FromBody] EmprestimoModel emprestimoModel,
            [FromServices] AppDbContext context)
        {
            context.Emprestimo!.Add(emprestimoModel);
            context.SaveChanges();
            return Created($"/{emprestimoModel.EmprestimoID}", emprestimoModel);
        }

        [HttpPut("/Emprestimo/Edit/{id:int}")]
        public IActionResult Put([FromRoute] int id, 
            [FromBody] EmprestimoModel emprestimoModel,
            [FromServices] AppDbContext context)
        {
            var model = context.Emprestimo!.Include(b => b.Bibliotecario).Include(l => l.Livro).FirstOrDefault(x => x.EmprestimoID == id);
            if (model == null) {
                return NotFound();
            }

            model.DataEmprestimo = emprestimoModel.DataEmprestimo;
            model.DataDevolucaoPrevista = emprestimoModel.DataDevolucaoPrevista;
            model.BibliotecarioID = emprestimoModel.BibliotecarioID;
            model.LivroID = emprestimoModel.LivroID;
            model.NomeUsuario = emprestimoModel.NomeUsuario;

            context.Emprestimo!.Update(model);
            context.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("/Emprestimo/Delete/{id:int}")]
        public IActionResult Delete([FromRoute] int id, 
            [FromServices] AppDbContext context)
        {
            var model = context.Emprestimo!.FirstOrDefault(x => x.EmprestimoID == id);
            if (model == null) {
                return NotFound();
            }

            context.Emprestimo!.Remove(model);
            context.SaveChanges();
            return Ok(model);
        }
    }
}