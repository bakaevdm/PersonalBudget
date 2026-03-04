using PersonalBudget;
using System.Transactions;

var newTrans = new PersonalBudget.Transaction
{
    Id = 1,
    Amount = 250.50m, // Буква 'm' в конце обязательна для типа decimal!
    Category = "Еда",
    Date = DateTime.Now,
    Description = "Кофе в аэропорту"
};

Console.WriteLine($"Купил {newTrans.Category} на сумму {newTrans.Amount} руб. ({newTrans.Description})");