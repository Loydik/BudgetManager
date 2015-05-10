using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.ViewModel.Util;
using System.Collections.ObjectModel;
using BudgetManager.Model.Db;
using BudgetManager.Model.Managers;
using System.Windows.Input;
using BudgetManager.View.TransactionsTab;
using System.Windows;

namespace BudgetManager.ViewModel.Transactions
{
    public class TransactionsControlViewModel : ObservableObject, IPageViewModel
    {   
        private TransactionsManager _transManager;
        private ObservableCollection<TransactionViewModel> _allTransactions;
        private ICommand _openCreateNewTransactionWindowCommand;
        private ICommand _refreshCommand;
        private ICommand _deleteTransactionCommand;
        private IWindowFactory _windowFactory;

        public String Name
        {
            get { return "Transactions"; }
        }

        public ObservableCollection<TransactionViewModel> AllTransactions
        {
            get
            { return _allTransactions; }
            set
            {
                _allTransactions = value;
                OnPropertyChanged("AllTransactions");
            }
        }


        public TransactionsControlViewModel()
        {
            _transManager = new TransactionsManager();
            this.init();
            _windowFactory = new ProductionWindowFactory();
        }

        public ICommand OpenCreateNewTransactionWindowCommand
        {
            get
            {
                if (_openCreateNewTransactionWindowCommand == null)
                {
                    _openCreateNewTransactionWindowCommand = new RelayCommand(
                        param => _windowFactory.CreateNewWindow(new CreateNewTransactionViewModel(), new CreateNewTransactionWindow())//, param => CreateNewTransactionCanExecute()
                    );
                }
                return _openCreateNewTransactionWindowCommand;
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                {
                    _refreshCommand = new RelayCommand(
                        param => Refresh()
                    );
                }
                return _refreshCommand;
            }
        }

        private void Refresh()
        {
            _transManager.updateTransactions();
            this.init();
        }

        public ICommand DeleteTransactionCommand
        {
            get
            {
                if (_deleteTransactionCommand == null)
                {
                    _deleteTransactionCommand = new RelayCommand(
                        param => DeleteTransaction((TransactionViewModel)param)
                    );
                }
                return _deleteTransactionCommand;
            }
        }

        private void DeleteTransaction(TransactionViewModel trans)
        {
            _transManager.deleteTransaction((int)trans.TransactionID);
            Refresh();
        }


        private void init()
        {
            var sortedTransactions = _transManager.Transactions.OrderByDescending(n => n.ID).ToList();
            AllTransactions = Util.ConversionHelper.toObservableCollection<TransactionViewModel, Transaction>(sortedTransactions, l => new TransactionViewModel(l));//getting data from manager and converting into Observable list
        }

      

    }
}
