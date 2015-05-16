using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.ViewModel.Util;
using System.Windows.Input;
using BudgetManager.ViewModel.Transactions;
using BudgetManager.ViewModel.Accounts;
using BudgetManager.ViewModel.Calendar;
using BudgetManager.ViewModel.Reports;

namespace BudgetManager.ViewModel.MainWindow
{
    public class MainWindowViewModel : ObservableObject
    {

        private ICommand _changePageCommand;
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }
 

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }

        public IPageViewModel TransactionsVm
        {
            get { return PageViewModels.SingleOrDefault(n => n.Name == "Transactions"); }
        }

        public IPageViewModel AccountsVm
        {
            get { return PageViewModels.SingleOrDefault(n => n.Name == "Accounts"); }
        }

        public IPageViewModel CalendarVm
        {
            get { return PageViewModels.SingleOrDefault(n => n.Name == "Calendar"); }
        }

        public IPageViewModel ReportsVm
        {
            get { return PageViewModels.SingleOrDefault(n => n.Name == "Reports"); }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

       public MainWindowViewModel()
       {
            // Add available pages
            PageViewModels.Add(new TransactionsControlViewModel());
            PageViewModels.Add(new AccountsControlViewModel());
            PageViewModels.Add(new CalendarControlViewModel());
            PageViewModels.Add(new ReportsControlViewModel());

            // Set starting page
            CurrentPageViewModel = PageViewModels[0];
            
        }


        #region Methods

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        #endregion

    }
}
