namespace BudgetPlaner.Application.Transactions.Queries.GetBalance;

public record BalanceDto
(
    decimal TotalIncome,
    decimal TotalExpense,
    decimal CurrentBalance
);