using ExpenseTracker.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public List<ExpenseIcon> ExpenseIconsList;
        public AddExpensePage()
        {
            InitializeComponent();
            ExpenseIconsList = new List<ExpenseIcon>
            {
                new ExpenseIcon
                {
                    IconFile = "/Assets/ExpenseIcons/Bills.png",
                    IconName = "Bills"
                },
                new ExpenseIcon
                {
                    IconFile = "/Assets/ExpenseIcons/Fuel.png",
                    IconName = "Fuel"
                },
                new ExpenseIcon
                {
                    IconFile = "/Assets/ExpenseIcons/Grocery.png",
                    IconName = "Grocery"
                },
                new ExpenseIcon
                {
                    IconFile = "/Assets/ExpenseIcons/Leisure.png",
                    IconName = "Leisure"
                },
                new ExpenseIcon
                {
                    IconFile = "/Assets/ExpenseIcons/Misc.png",
                    IconName = "Misc"
                },
                new ExpenseIcon
                {
                    IconFile = "/Assets/ExpenseIcons/Restaurant.png",
                    IconName = "Food"
                },
                new ExpenseIcon
                {
                    IconFile = "/Assets/ExpenseIcons/Shopping.png",
                    IconName = "Shopping"
                },
                new ExpenseIcon
                {
                    IconFile = "/Assets/ExpenseIcons/Travel.png",
                    IconName = "Travel"
                }
            };
            ExpenseIcons.ItemsSource = ExpenseIconsList;
        }

        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async private void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            var expense = (Expense)BindingContext;
            expense.Amount = Convert.ToDecimal(ExpenseAmount.Text);
            expense.Date = ExpenseDate.Date;
            expense.Name = ExpenseName.Text;
            expense.Category = (ExpenseIcon)ExpenseIcons.SelectedItem;
            
            expense.FileName = Path.Combine
                (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{Path.GetRandomFileName()}.Expense.json");
            String jsonString = JsonConvert.SerializeObject(expense);
            File.WriteAllText(expense.FileName, jsonString);
            await DisplayAlert($"{ExpenseName.Text}: ${ExpenseAmount.Text}", "Expense has been added.", "OK");
            await Navigation.PopModalAsync();
        }
    }
}