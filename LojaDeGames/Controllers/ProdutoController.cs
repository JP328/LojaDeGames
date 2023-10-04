using FluentValidation;
using LojaDeGames.Model;
using LojaDeGames.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaDeGames.Controllers
{
    [ApiController, Route("~/produtos"), Authorize]
    public class ProdutoController : ControllerBase
    {
        private readonly IProduto _produtoService;
        private readonly IValidator<Produto> _produtoValidator;

        public ProdutoController(IProduto produto, IValidator<Produto> validator)
        {
            _produtoService = produto;
            _produtoValidator = validator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _produtoService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById (long id)
        {
            var BuscarProdutos = await _produtoService.GetById(id);

            if (BuscarProdutos is null)
                return NoContent();

            return Ok(BuscarProdutos);
        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult> GetByName(string nome)
        {
            return Ok(await _produtoService.GetByName(nome));
        }

        [HttpGet("preco/{min}/{max}")]
        public async Task<ActionResult> GetByPriceRange(decimal min, decimal max)
        {
            return Ok(await _produtoService.GetBetweenPrices(min, max));
        }

        [HttpGet("nome/{nome}/ouconsole/{console}")]
        public async Task<ActionResult> GetByNameOrConsole(string nome, string console)
        {
            return Ok(await _produtoService.GetByNameOrConsole(nome, console));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Produto produto)
        {
            var validarProduto = await _produtoValidator.ValidateAsync(produto);

            if (!validarProduto.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarProduto);

            var Resposta = await _produtoService.Create(produto);

            if (Resposta is null)
                return BadRequest("Categoria não encontrada.");

            return CreatedAtAction(nameof(GetById), new {id =  produto.Id}, produto);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Produto produto)
        {
            if (produto.Id == 0)
                return BadRequest("Id do produto inválido");

            var validarProduto = await _produtoValidator.ValidateAsync(produto);

            if (!validarProduto.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarProduto);

            var Resposta = await _produtoService.Update(produto);

            if (Resposta is null)
                return NotFound("Produto não encontrado");

            return Ok(Resposta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (long id)
        {
            var BuscarProduto = await _produtoService.GetById(id);

            if (BuscarProduto is null)
                return NotFound("Esse produto não foi encontrado");

            await _produtoService.Delete(BuscarProduto);

            return NoContent();
        }

    }
}
