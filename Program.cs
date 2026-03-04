using PersonalBudget;
using System.Transactions;
using System.Text.RegularExpressions;


var dataTransaction = new List<PersonalBudget.Transaction>();
int id = 0;

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
            
            Console.WriteLine();
            Console.Write("Нажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();            
            break;
        case 2:
            Console.Clear();
            Console.WriteLine("Операции");
            

            
            Console.WriteLine();
            Console.Write("Нажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();            
            break;
        case 3:
            Console.Clear();
            Console.WriteLine("Текущий баланс");



            Console.Write("Нажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
            break;
        default:            
            Console.WriteLine("Не опознанное действие");
            Console.Write("Нажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
            break;
    }
}
