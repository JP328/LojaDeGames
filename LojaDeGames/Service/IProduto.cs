using LojaDeGames.Model;

namespace LojaDeGames.Service
{
    public interface IProduto
    {
        Task<IEnumerable<Produto>> GetAll();

        Task<Produto?> GetById (long id);

        Task<IEnumerable<Produto>?> GetByName (string nome);

        Task<IEnumerable<Produto>> GetByNameOrConsole(string nome, string console);

        Task<IEnumerable<Produto>> GetBetweenPrices(decimal min, decimal max);

        Task<Produto?> Create (Produto produto);

        Task<Produto?> Update (Produto produto);

        Task Delete (Produto produto);
    }
}
