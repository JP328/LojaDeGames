﻿using LojaDeGames.Model;

namespace LojaDeGames.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();

        Task<User?> GetById(long id);

        Task<User?> GetByUser(string usuario);

        Task<User?> Create(User usuario);

        Task<User?> Update(User usuario);
    }
}