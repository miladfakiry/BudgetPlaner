using FluentValidation;

namespace BudgetPlaner.Application.Transactions.Commands.UpdateTransaction
{
    public class UpdateTransactionCommandValidator : AbstractValidator<UpdateTransactionCommand>
    {
        public UpdateTransactionCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("ID ist erforderlich.");
            RuleFor(v => v.Description)
                .NotEmpty().WithMessage("Beschreibung darf nicht leer sein.")
                .MaximumLength(200).WithMessage("Beschreibung darf maximal 200 Zeichen lang sein.");
            RuleFor(v => v.Amount)
                .GreaterThan(0).WithMessage("Betrag muss positiv sein.");
            RuleFor(v => v.Date)
                .LessThanOrEqualTo(DateTime.Now.AddDays(1)).WithMessage("Transaktionen können nicht in der Zukunft liegen.");
            RuleFor(v => v.CategoryId)
                .NotEmpty().WithMessage("Kategorie ist erforderlich.");
        }
    }
}
