using FluentValidation;
using LojaDeGames.Model;

namespace LojaDeGames.Validator
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator() 
        {
            RuleFor(p => p.nome)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(255);

            RuleFor(p => p.descricao)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(2000);

            RuleFor(p => p.console)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(255);

            RuleFor(p => p.DataLancamento)
                .NotEmpty();

            RuleFor(p => p.preco)
                .GreaterThan(0)
                .PrecisionScale(8,2, false);

            RuleFor(p => p.foto)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(2000);
        }
    }
}
