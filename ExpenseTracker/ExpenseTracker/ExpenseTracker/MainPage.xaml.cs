using ExpenseTracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpenseTracker
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Expense> expenses;
        String budgetFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MonthlyBudget.txt");
        public MainPage()
        {
            InitializeComponent();
            AddExpenseButton.IsVisible = false;
            
            if (File.Exists(budgetFileName))
            {
                Budget.Text = File.ReadAllText(budgetFileName);
                SaveButton.IsVisible = false;
                Budget.IsReadOnly = true;
                AddExpenseButton.IsVisible = true;
            }
        }
        
        protected override void OnAppearing()
        {
            expenses = new ObservableCollection<Expense>();
            List<Expense> expensesList= ExpenseManager.GetExpenses();
            
            expensesList.ForEach(expense => expenses.Add(expense));
            Decimal spentAmount;
            if (!String.IsNullOrEmpty(Budget.Text))
            {
                spentAmount = expenses.Sum(n => n.Amount);
                TotalExpenses.Text = $"{spentAmount.ToString()}";

                Balance.Text = $"{(Convert.ToDecimal(Budget.Text)-spentAmount).ToString()}";
            }
            MyExpensesList.ItemsSource = expenses;
        }

        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (!File.Exists(budgetFileName))
            {
                File.WriteAllText(budgetFileName, Budget.Text);
                Budget.Text = $"{File.ReadAllText(budgetFileName)}";
                SaveButton.IsVisible = false;
                Budget.IsReadOnly = true;
                AddExpenseButton.IsVisible = true;
                Balance.Text = $"{Budget.Text}";
            }
            else
            {
                Budget.Text = $"{File.ReadAllText(budgetFileName)}";
            }
        }

    

        private async void OnAddExpenseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddExpensePage {BindingContext=new Expense() });
        }

        /*private void OnDeleteExpenseButtonClicked(object sender, EventArgs e)
        {
           var button = sender as Button;
            Expense expense=button.BindingContext as Expense;
            File.Delete(expense.FileName);
            expenses.Clear();
           List<Expense> expensesList = ExpenseManager.GetExpenses();
            expensesList.ForEach(expense1 => expenses.Add(expense1));
        }*/

        async private void MyExpensesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Expense SelectedExpense = (Expense)MyExpensesList.SelectedItem;
            string action = await DisplayActionSheet($"{SelectedExpense.Name}: ${SelectedExpense.Amount}\n{SelectedExpense.Date}",
                "Cancel",
                "Delete");

            if (action == "Delete")
            {
                File.Delete(SelectedExpense.FileName);
                expenses.Clear();
                List<Expense> expensesList = ExpenseManager.GetExpenses();
                expensesList.ForEach(expense1 => expenses.Add(expense1));
                Decimal spentAmount;
                if (!String.IsNullOrEmpty(Budget.Text))
                {
                    spentAmount = expenses.Sum(n => n.Amount);
                    TotalExpenses.Text = $"{spentAmount.ToString()}";

                    Balance.Text = $"{(Convert.ToDecimal(Budget.Text) - spentAmount).ToString()}";
                }
            }

        }
    }
}
