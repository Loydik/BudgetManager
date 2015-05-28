using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.Model.Db;
using BudgetManager.Model.ReportGenerators;

namespace BudgetManager.Model.Managers
{
    public class VisualizationManager
    {
        private ReportsManager _reportsManager;
        private TransactionsManager _transactionsManager;

        public VisualizationManager()
        {
            _reportsManager = new ReportsManager();
            _transactionsManager = new TransactionsManager();
        }

        public List<CategoryDisplayClass> GetVisualCategoriesForSpendings(TimePeriod timePeriod)
        {
            List<Category> usedCategories = _reportsManager.GetUsedCategories(timePeriod);
            List<CategoryDisplayClass> visualCategories = new List<CategoryDisplayClass>();

            foreach (var category in usedCategories)
            {
                CategoryDisplayClass cdc = new CategoryDisplayClass {CategoryName = category.Name};
                decimal total = 0;

                //getting withdrawal transactions for a category in specified time period
                var transactions = _transactionsManager.Transactions.Where(n => n.Category.Name == category.Name && n.TransactionType.Name == "Withdrawal" && n.Date >= timePeriod.StartDate && n.Date <= timePeriod.EndDate).ToList();

                foreach (var transaction in transactions)
                {
                    total = total + transaction.Amount;
                }

                cdc.Number = total;
                visualCategories.Add(cdc);
            }

            return visualCategories;
        }

        public List<CategoryDisplayClass> GetVisualCategoriesForIncome(TimePeriod timePeriod)
        {
            List<Category> usedCategories = _reportsManager.GetUsedCategories(timePeriod);
            List<CategoryDisplayClass> visualCategories = new List<CategoryDisplayClass>();

            foreach (var category in usedCategories)
            {
                CategoryDisplayClass cdc = new CategoryDisplayClass { CategoryName = category.Name };
                decimal total = 0;

                //getting income transactions for a category in specified time period
                var transactions = _transactionsManager.Transactions.Where(n => n.Category.Name == category.Name && n.TransactionType.Name == "Deposit" && n.Date >= timePeriod.StartDate && n.Date <= timePeriod.EndDate).ToList();

                foreach (var transaction in transactions)
                {
                    total = total + transaction.Amount;
                }

                cdc.Number = total;
                visualCategories.Add(cdc);
            }

            return visualCategories;
        }

        public void UpdateTransactions()
        {
            _transactionsManager.UpdateTransactions();
        }

        //Class which will represent the data to be plotted
        public class CategoryDisplayClass
        {
            public string CategoryName { get; set; }

            public decimal Number { get; set; }
        }

    }
}
