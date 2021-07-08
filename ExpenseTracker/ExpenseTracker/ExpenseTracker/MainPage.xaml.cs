using ExpenseTracker.Model;
using System;
using System.Collections.Generic;
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
        String budgetFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MonthlyBudget.txt");
        public MainPage()
        {
            InitializeComponent();
            if (File.Exists(budgetFileName))
            {
                Budget.Text = File.ReadAllText(budgetFileName);
                SaveButton.IsVisible = false;
                Budget.IsReadOnly = true;
               
                
            }
        }
        protected override void OnAppearing()
        {
            MyExpensesList.ItemsSource=ExpenseManager.GetExpenses();
        }

        private void OnSaveButtonClicked(object sender, EventArgs e)
        {

            if (!File.Exists(budgetFileName))
            {
                File.WriteAllText(budgetFileName, Budget.Text);
                Budget.Text = File.ReadAllText(budgetFileName);
            }
            else
                Budget.Text = File.ReadAllText(budgetFileName);

        }

        private async void OnAddExpenseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddExpensePage {BindingContext=new Expense() });
        }
    }
}
