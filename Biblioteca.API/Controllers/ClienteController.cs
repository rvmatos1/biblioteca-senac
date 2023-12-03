using Biblioteca.API.Models;
using Biblioteca.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        [Route("/Cliente")]
        public IActionResult Get(
            [FromServices] AppDbContext context) =>
                Ok(context.Cliente!.ToList());

        [HttpGet("/Cliente/Details/{id:int}")]
        public IActionResult GetById([FromRoute] int id,
            [FromServices] AppDbContext context)
        {
            var clienteModel = context.Cliente!.FirstOrDefault(x => x.ClienteID == id);
            if (clienteModel == null)
            {
                return NotFound();
            }

            return Ok(clienteModel);
        }

        [HttpPost("/Cliente/Create")]
        public IActionResult Post([FromBody] ClienteModel clienteModel,
            [FromServices] AppDbContext context)
        {
            context.Cliente!.Add(clienteModel);
            context.SaveChanges();
            return Created($"/{clienteModel.ClienteID}", clienteModel);
        }

        [HttpPut("/Cliente/Edit/{id:int}")]
        public IActionResult Put([FromRoute] int id,
            [FromBody] ClienteModel clienteModel,
            [FromServices] AppDbContext context)
        {
            var model = context.Cliente!.FirstOrDefault(x => x.ClienteID == id);
            if (model == null)
            {
                return NotFound();
            }

            model.Nome = clienteModel.Nome;
            model.Email = clienteModel.Email;
            model.Cpf = clienteModel.Cpf;
            model.Telefone = clienteModel.Telefone;

            context.Cliente!.Update(model);
            context.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("/Cliente/Delete/{id:int}")]
        public IActionResult Delete([FromRoute] int id,
            [FromServices] AppDbContext context)
        {
            var model = context.Cliente!.FirstOrDefault(x => x.ClienteID == id);
            if (model == null)
            {
                return NotFound();
            }

            context.Cliente!.Remove(model);
            context.SaveChanges();
            return Ok(model);
        }

    }
}
