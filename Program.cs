using PersonalBudget;
using System.Transactions;
using System.Text.RegularExpressions;


var budgetManager = new BudgetManager(FileService.Load());

while (true)
{
    Console.Clear();
    Console.WriteLine("1. Добавить операцию");
    Console.WriteLine("2. Показать все операции");
    Console.WriteLine("3. Показать текущий баланс");
    Console.WriteLine("4. Удалить операцию");
    Console.WriteLine("5. Выход");
    string readAction;
    string pattern = @"^[1-5]$";

    do
    {
        readAction = Console.ReadLine();
    }
    while (!Regex.IsMatch(readAction, pattern));

    int numAction = int.Parse(readAction);

    if (numAction == 5)
        break;

    switch (numAction)
    {
        case 1:
            Console.Clear();            

            decimal amount;            
            do
            {
                Console.Write("Сумма: ");
                string input = Console.ReadLine();
                if (!input.StartsWith("+") && !input.StartsWith("-"))
                    Console.WriteLine("Сумма должна начинаться с действия +/-");
                else
                {
                    if (decimal.TryParse(input, out amount))
                        break;

                    Console.WriteLine("Сумма должна быть дробным числом");
                }
            }
            while (true);

            string category;
            do
            {
                Console.Write("Категория: ");
                category = Console.ReadLine();
                if (!string.IsNullOrEmpty(category))
                    break;

                Console.WriteLine("Необходимо указать категорию");
            }
            while (true);

            Console.Write("Комментарий: ");
            var comment = Console.ReadLine();

            Console.Clear();

            Console.WriteLine("Проверьте заполненые данные");
            Console.WriteLine($"Сумма: {amount}");
            Console.WriteLine($"Категория: {category}");
            Console.WriteLine($"Коммент: {comment}");
            Console.WriteLine();
            Console.WriteLine("Сохранить? (Да/Нет)");
            if (Console.ReadLine() == "Да")
                budgetManager.AddTransaction(amount, category, comment);
  
            break;
        case 2:
            Console.Clear();
            Console.WriteLine($"{"#", -4}|{"Дата",-12}|{"Категория",-20}|{"Сумма",-20}|{"Комментарий",-40}|{"Баланс",-15}");
            Console.WriteLine(new string('-', 111));

            decimal runningBalance = 0;
            foreach (var item in budgetManager.GetAllTransactions())
            {
                runningBalance += item.Amount;
                Console.ForegroundColor = item.Amount >= 0 ? ConsoleColor.Green : ConsoleColor.Red;

                string dateDisplay = item.Date == DateTime.Now.Date ? "Сегодня" :
                                     item.Date == DateTime.Now.Date.AddDays(-1) ? "Вчера" :
                                     item.Date.ToShortDateString();

                Console.Write($"{item.Id, -4}");
                Console.Write($"|{dateDisplay,-12}");
                Console.Write($"|{item.Category,-20}");
                Console.Write($"|{item.Amount,-20}");
                Console.Write($"|{item.Comment,-40}");
                Console.WriteLine($"|{runningBalance,-15}");

                Console.ResetColor();
            }                          
            break;
        case 3:
            Console.Clear();
            Console.Write("Текущий баланс: ");
            Console.WriteLine(budgetManager.GetBalance());            
            break;
        case 4:
            Console.Clear();
            int transactionId;
            do
            {
                Console.Write("Введите ИД операции: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out transactionId))
                {
                    if (budgetManager.TransactionExists(transactionId))
                    {
                        budgetManager.RemoveTransaction(transactionId);
                        Console.WriteLine($"Операция с ИД - {input} удалена");
                    }
                    else
                        Console.WriteLine($"Операция с ИД - {input} не найдена");
                    break;
                }
                else
                    Console.WriteLine("ИД должен быть целым числом");                     
            }
            while (true);

            break;
        default:            
            Console.WriteLine("Не опознанное действие");            
            break;
    }

    Console.WriteLine();
    Console.Write("Нажмите любую клавишу, чтобы продолжить...");
    Console.ReadKey();
}
