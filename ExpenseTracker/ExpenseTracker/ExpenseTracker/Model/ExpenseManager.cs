using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Model
{
    public static class ExpenseManager
    {
        public static List<Expense> GetExpenses()
        {
            List<Expense> expenses = new List<Expense>();

            expenses.Add(new Expense {Name="Fruits",Amount=10, Date=DateTime.Now, Category="Food" });
            expenses.Add(new Expense { Name = "Books", Amount = 30, Date = DateTime.Now, Category = "Misc" });
            return expenses;
        }
    }
}
