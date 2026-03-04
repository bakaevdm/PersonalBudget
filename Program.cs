using PersonalBudget;
using System.Transactions;
using System.Text.RegularExpressions;


var dataTransaction = new List<PersonalBudget.Transaction>();
int id = 1;

while (true)
{
    Console.Clear();
    Console.WriteLine("1. Добавить операцию");
    Console.WriteLine("2. Показать все операции");
    Console.WriteLine("3. Показать текущий баланс");
    Console.WriteLine("4. Выход");
    string readAction;
    string pattern = @"^[1-4]$";

    do
    {
        readAction = Console.ReadLine();
    }
    while (!Regex.IsMatch(readAction, pattern));

    int numAction = int.Parse(readAction);

    if (numAction == 4)
        break;

    switch (numAction)
    {
        case 1:
            Console.Clear();
            Console.WriteLine("Добавление");

            decimal amount;
            char action;
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
            Console.Write("Категория: ");
            var category = Console.ReadLine();
            Console.Write("Комментарий: ");
            var comment = Console.ReadLine();

            dataTransaction.Add(PersonalBudget.Transaction.Create(id++, amount, category, comment));
            break;
        case 2:
            Console.Clear();
            Console.WriteLine($"{"#", -4}{"Дата",-12}|{"Категория",-20}|{"Сумма",-20}|{"Комментарий",-15}|{"Баланс",-15}");
            Console.WriteLine(new string('-', 86));

            decimal runningBalance = 0;
            foreach (var item in dataTransaction.OrderBy(d => d.Id))
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
                Console.Write($"|{item.Comment,-15}");
                Console.WriteLine($"|{runningBalance,-15}");

                Console.ResetColor();
            }                          
            break;
        case 3:
            Console.Clear();
            Console.Write("Текущий баланс: ");
            Console.WriteLine(dataTransaction.Select(d => d.Amount).Sum().ToString());            
            break;
        default:            
            Console.WriteLine("Не опознанное действие");
            
            break;
    }

    Console.WriteLine();
    Console.Write("Нажмите любую клавишу, чтобы продолжить...");
    Console.ReadKey();
}
