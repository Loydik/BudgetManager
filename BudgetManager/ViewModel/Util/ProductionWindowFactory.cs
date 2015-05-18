using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BudgetManager.ViewModel.Reports;
using Microsoft.Win32;

namespace BudgetManager.ViewModel.Util
{
    public class ProductionWindowFactory : IWindowFactory
    {
        #region Implementation of INewWindowFactory

        public void CreateNewWindow(ObservableObject viewModel, Window window)
        {
            window.DataContext = viewModel;
            window.ShowDialog();
        }

        #endregion
    }
}
