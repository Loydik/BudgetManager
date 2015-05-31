using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BudgetManager.ViewModel.Util
{
    public interface IWindowFactory
    {
        void CreateNewWindow(ObservableObject viewModel, Window window);
        void ShowMessage(String message);
    }
}
