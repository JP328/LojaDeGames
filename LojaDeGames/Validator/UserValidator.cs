using FluentValidation;
using LojaDeGames.Model;

namespace LojaDeGames.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() 
        {
            RuleFor(u => u.Nome)
              .NotEmpty()
              .MaximumLength(255);

            RuleFor(u => u.Usuario)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);

            RuleFor(u => u.Senha)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(255);

            RuleFor(u => u.Foto)
                .MaximumLength(5000);

            RuleFor(u => u.DataDeNascimento)
                .NotEmpty()
                .Must(BeAValidateAge)
                .WithMessage("Você precisa ser maior de 18 para se cadastrar");
        }

        protected bool BeAValidateAge (DateOnly date)
        {
            int anoAtual = DateTime.Now.Year;
            int anoDeNascimento = date.Year;

            if(anoDeNascimento <= anoAtual && anoDeNascimento > (anoAtual - 120) && (anoAtual - anoDeNascimento) >= 18)
                return true;

            return false;
        }
    }
}
