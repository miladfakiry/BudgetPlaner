namespace BudgetPlaner.Application.Transactions.Queries.GetTransactions
{
    public record TransactionDto(

        Guid Id,
        string Description,
        decimal Amount,
        DateTime Date,
        string Type,
        string CategoryName
    );
}
