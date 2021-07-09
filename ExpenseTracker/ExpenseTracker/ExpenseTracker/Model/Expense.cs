using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Model
{
    public class Expense
    {
        public String Name { get; set; }
        public Decimal Amount { get; set; }
        public DateTime Date{ get; set; }
        public ExpenseIcon Category { get; set; }
        public String FileName { get; set; }



    }
}
