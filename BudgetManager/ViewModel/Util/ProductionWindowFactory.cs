using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BudgetManager.ViewModel.Util
{
    public class ProductionWindowFactory : IWindowFactory
    {
        #region Implementation of INewWindowFactory

        public void CreateNewWindow(ObservableObject viewModel, Window window)
        {
            window.DataContext = viewModel;
            window.Show();
        }

        #endregion
    }
}
