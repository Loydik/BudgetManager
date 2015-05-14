using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BudgetManager.Model.Db;
using BudgetManager.ViewModel.Transactions;

namespace BudgetManager.View.CalendarTab
{
    /// <summary>
    /// Gets the transactions for the specified date.
    /// </summary>
    [ValueConversion(typeof (ObservableCollection<TransactionViewModel>),
        typeof (ObservableCollection<TransactionViewModel>))]
    public class TransactionsConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            DateTime date = (DateTime) values[1];

            ObservableCollection<TransactionViewModel> transactions = new ObservableCollection<TransactionViewModel>();
            foreach (TransactionViewModel transaction in (ObservableCollection<TransactionViewModel>) values[0])
            {
                if (transaction.Date.Date == date)
                {
                    transactions.Add(transaction);
                }
            }

            return transactions;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
