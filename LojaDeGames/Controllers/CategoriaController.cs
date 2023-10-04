using FluentValidation;
using LojaDeGames.Model;
using LojaDeGames.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaDeGames.Controllers
{
    [ApiController, Route("~/categorias"), Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoria _categoriaService;
        private readonly IValidator<Categoria> _categoriaValidator;

        public CategoriaController(ICategoria categoria, IValidator<Categoria> validator)
        {
            _categoriaService = categoria;
            _categoriaValidator = validator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _categoriaService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id) 
        {
            var Resposta = await _categoriaService.GetById(id);

            if (Resposta is null) 
                return NotFound();

            return Ok(Resposta);
        }

        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult> GetByTipo(string tipo) 
        {
            return Ok(await _categoriaService.GetByTipo(tipo));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Categoria categoria)
        {
            var validarCategoria = await _categoriaValidator.ValidateAsync(categoria);

            if(!validarCategoria.IsValid)
                return BadRequest(validarCategoria.Errors);

            await _categoriaService.Create(categoria);

            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Categoria categoria)
        {
            if (categoria.Id == 0)
                return BadRequest("Id da categoria inválido");

            var ValidarCategoria = await _categoriaValidator.ValidateAsync(categoria);

            if (!ValidarCategoria.IsValid)
                return BadRequest(ValidarCategoria.Errors);

            var Resposta = await _categoriaService.Update(categoria);

            if (Resposta is null)
                return BadRequest("Categoria não encontrado");

            return Ok(Resposta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var BuscaId = await _categoriaService.GetById(id);

            if (BuscaId is null)
                return BadRequest();

            await _categoriaService.Delete(BuscaId);
            return NoContent();
        }
    }
}
