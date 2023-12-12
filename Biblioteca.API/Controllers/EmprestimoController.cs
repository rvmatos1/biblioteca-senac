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
            var emprestimos = context.Emprestimo!.Where(x => x.Ativo).Include(b => b.Bibliotecario).Include(c => c.Cliente).Include(l => l.Livro).ToList();
            return Ok(emprestimos);
        }

        [HttpGet("/Emprestimo/Details/{id:int}")]
        public IActionResult GetById([FromRoute] int id, [FromServices] AppDbContext context)
        {
            var emprestimoModel = context.Emprestimo!.Include(b => b.Bibliotecario).Include(l => l.Livro).Include(c => c.Cliente).FirstOrDefault(x => x.EmprestimoID == id);
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
                Cliente = new
                {
                    ID = emprestimoModel.Cliente!.ClienteID,
                    Nome = emprestimoModel.Cliente.Nome,
                    Email = emprestimoModel.Cliente.Email,
                    Telefone = emprestimoModel.Cliente.Telefone,
                    Cpf = emprestimoModel.Cliente.Cpf
                }                
            });
        }

        [HttpPost("/Emprestimo/Create")]
        public IActionResult Post([FromBody] EmprestimoModel emprestimoModel,
            [FromServices] AppDbContext context)
        {
            var livro = context.Livro!.FirstOrDefault(x => x.LivroID == emprestimoModel.LivroID);

            if (livro == null || livro?.QuantidadeDisponivel <= 0)
            {
                return NotFound("Livro não encontrado no acervo ou não disponível para empréstimo!");
            }
            livro.QuantidadeDisponivel--;
            context.Livro!.Update(livro);

            emprestimoModel.Ativo = true;
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
            model.ClienteID = emprestimoModel.ClienteID;

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

            var livro = context.Livro!.FirstOrDefault(x => x.LivroID == model.LivroID);
            if(livro != null)
            {
                livro.QuantidadeDisponivel++;
                context.Livro!.Update(livro);
            }

            model.Ativo = false;

            context.Emprestimo!.Update(model);
            context.SaveChanges();
            return Ok(model);
        }
    }
}