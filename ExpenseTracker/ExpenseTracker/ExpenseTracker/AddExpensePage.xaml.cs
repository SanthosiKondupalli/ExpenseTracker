using ExpenseTracker.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddExpensePage : ContentPage
    {
        public AddExpensePage()
        {
            InitializeComponent();
        }

        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            var expense = (Expense)BindingContext;
            expense.Amount =Convert.ToDecimal(ExpenseAmount.Text);
            expense.Date = ExpenseDate.Date;
            expense.Name = ExpenseName.Text;
            //expense.Category=
            String jsonString = JsonConvert.SerializeObject(expense);
            expense.FileName= Path.Combine
                (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{Path.GetRandomFileName()}.Expense.json");
            File.WriteAllText(expense.FileName, jsonString);
            Navigation.PopModalAsync();
        }
    }
}