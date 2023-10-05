using LojaDeGames.Data;
using LojaDeGames.Model;
using Microsoft.EntityFrameworkCore;

namespace LojaDeGames.Service.Implements
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users
             .ToListAsync();
        }

        public async Task<User?> GetById(long id)
        {
            try
            {
                var User = await _context.Users
                    .FirstAsync(u => u.Id == id);

                User.Senha = "";

                return User;
            }
            catch { return null; }
        }

        public async Task<User?> GetByUser(string usuario)
        {
            var BuscaUsuario = await _context.Users
                .Where(u => u.Usuario == usuario)
                .FirstOrDefaultAsync();

            return BuscaUsuario;
        }

        public async Task<User?> Create(User usuario)
        {
            var BuscarUsuario = await GetByUser(usuario.Usuario);

            if (BuscarUsuario is not null)
                return null;

            if (usuario.Foto is null || usuario.Foto == "")
                usuario.Foto = "https://i.imgur.com/I8MfmC8.png";

            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha, workFactor: 10);

            await _context.Users.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<User?> Update(User usuario)
        {
            var UserUpdate = await _context.Users.FindAsync(usuario.Id);

            if (UserUpdate is null)
                return null;

            if (usuario.Foto is null || usuario.Foto == "")
                usuario.Foto = "https://i.imgur.com/I8MfmC8.png";

            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha, workFactor: 10);

            _context.Entry(UserUpdate).State = EntityState.Detached;
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Produto?> Curtir(long id)
        {
            var Produto = await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (Produto is null) 
                return null;

            Produto.curtir += 1;

            _context.Update(Produto);
            return Produto;
        }
    }
}
