using LojaDeGames.Data;
using LojaDeGames.Model;
using Microsoft.EntityFrameworkCore;

namespace LojaDeGames.Service.Implements
{
    public class ProdutoService : IProduto
    {
        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos
                .Include(p => p.Categoria)
                .ToListAsync();
        }

        public async Task<Produto?> GetById(long Id)
        {
            try
            {
                var Produto = await _context.Produtos
                    .Include(p => p.Categoria)
                    .Where(p => p.Id == Id)
                    .FirstAsync();

                return Produto;
            } catch { return null; }
        }

        public async Task<IEnumerable<Produto>?> GetByName(string nome)
        {
            var Produto = await _context.Produtos
                .Include(p => p.Categoria)
                .Where(p => p.nome.Contains(nome))
                .ToListAsync();

            return Produto;
        }

        public async Task<IEnumerable<Produto>> GetByNameOrConsole(string nome, string console)
        {
            var Produto = await _context.Produtos
                .Include(p => p.Categoria)
                .Where(p => p.nome.Contains(nome) || p.console.Contains(console))
                .ToListAsync();

            return Produto;
        }

        public async Task<IEnumerable<Produto>> GetBetweenPrices(decimal min, decimal max)
        {
            var Produto = await _context.Produtos
                .Include(p => p.Categoria)
                .Where(p => p.preco >= min && p.preco <= max)
                .ToListAsync();
           
            return Produto;
        }

        public async Task<Produto?> Create(Produto produto)
        {
            if (produto.Categoria is not null)
            {
                var VerificarCategoria = await _context.Categorias.FindAsync(produto.Categoria.Id);

                if (VerificarCategoria is null)
                    return null;

                produto.Categoria = VerificarCategoria;
            }

            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto?> Update(Produto produto)
        {
            var ProdutoUpdate = await _context.Produtos.FindAsync(produto.Id);

            if (ProdutoUpdate is null)
                return null;

            if (produto.Categoria is not null)
            {
                var VerificarCategoria = await _context.Categorias.FindAsync(produto.Categoria.Id);

                if (VerificarCategoria is null)
                    return null;

                produto.Categoria = VerificarCategoria;
            }

            _context.Entry(ProdutoUpdate).State = EntityState.Detached;
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task Delete(Produto produto)
        {
            _context.Remove(produto);
            await _context.SaveChangesAsync();
        }

    }
}
