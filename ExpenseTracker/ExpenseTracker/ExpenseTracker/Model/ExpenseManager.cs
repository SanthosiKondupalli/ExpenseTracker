using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExpenseTracker.Model
{
    public static class ExpenseManager
    {
        public static List<Expense> GetExpenses()
        {
            List<Expense> expenses = new List<Expense>();
            var files = Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "*.Expense.json");
            foreach(var fileName in files)
            {
                String jsonString = File.ReadAllText(fileName);
                expenses.Add(JsonConvert.DeserializeObject<Expense>(jsonString));
            }
            
            return expenses.OrderBy(n=>n.Date).ToList<Expense>();
        }
        public static void DeleteExpense(int position)
        {

        }
    }
}
