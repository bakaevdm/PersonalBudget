using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudget
{
    public class BudgetManager
    {
        private List<Transaction> _transactions;

        private int _nextId;

        public BudgetManager(List<Transaction> initData)
        {
            _transactions = initData;
            _nextId = _transactions.Any() ? _transactions.Max(t => t.Id) + 1 : 1;
        }

        public void AddTransaction(decimal amount, string category, string comment)
        {
            _transactions.Add(Transaction.Create(_nextId++, amount, category, comment));
            FileService.Save(_transactions);
        }

        public List<Transaction> GetAllTransactions() => _transactions.OrderBy(d => d.Id).ToList();

        public decimal GetBalance() => _transactions.Select(d => d.Amount).Sum();
    }
}
