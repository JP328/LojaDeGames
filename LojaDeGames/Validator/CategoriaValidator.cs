using FluentValidation;
using LojaDeGames.Model;

namespace LojaDeGames.Validator
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator() 
        {
            RuleFor(c => c.tipo)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(255);
        }
    }
}
