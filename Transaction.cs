using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudget
{
    public class Transaction
    {
        public int Id { get; set; }

        /// <summary>
        /// Расход записывается со знаком -, доход — со знаком +.
        /// </summary>
        public decimal Amount { get; set; }
        
        public string Category { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public static Transaction Create(int id, decimal amount, string category, string comment)
        {
            return new Transaction
            {
                Id = id,
                Amount = amount,                
                Category = category,
                Comment = comment,
                Date = DateTime.Now
            };
        }
    }
}
