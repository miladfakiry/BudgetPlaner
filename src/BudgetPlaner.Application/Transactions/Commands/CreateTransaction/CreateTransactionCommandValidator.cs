using FluentValidation;

namespace BudgetPlaner.Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(v => v.Description)
                .NotEmpty().WithMessage("Die Beschreibung darf nicht leer sein.")
                .MaximumLength(200).WithMessage("Die Beschreibung darf maximal 200 Zeichen lang sein.");

            RuleFor(v => v.Amount)
                .GreaterThan(0).WithMessage("Der Betrag muss positiv sein.");

            RuleFor(v => v.Date)
                .LessThanOrEqualTo(DateTime.Now.AddDays(1)).WithMessage("Transaktionen können nicht in der Zukunft liegen.");

            RuleFor(v => v.CategoryId)
                .NotEmpty().WithMessage("Eine Kategorie ist erforderlich.");
        }
    }
}
