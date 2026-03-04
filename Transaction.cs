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
        public int Id {  get; set; }

        public decimal Amount { get; set; }

        public string Category { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
    }
}
