using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BudgetManager.Model.Db;
using BudgetManager.Model.Managers;
using BudgetManager.ViewModel.Util;

namespace BudgetManager.ViewModel.Settings
{
    public class SettingsControlViewModel : ObservableObject, IPageViewModel
    {
        private TransactionsManager _transManager;
        private ObservableCollection<CategoryViewModel> _categories;
        private CategoryViewModel _selectedCategory;
        private string _newCategoryName;
        private ICommand _addNewCategoryNameCommand;
        private ICommand _deleteCategoryCommand;
        private IWindowFactory _windowFactory;

        public string Name {
            get { return "Settings"; }
        }

        public SettingsControlViewModel()
        {
            _transManager = new TransactionsManager();
            _windowFactory = new ProductionWindowFactory();
            Init();
        }

        private void Init()
        {
            var sortedCategories = _transManager.TransactionCategories.Where(n=>n.Name != "Uncategorized").OrderBy(n => n.Name).ToList();
            Categories = ConversionHelper.ToObservableCollection(sortedCategories, l => new CategoryViewModel(l));//getting data from manager and converting into Observable list
        }

        public ObservableCollection<CategoryViewModel> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged("Categories");
            }
        }

        public CategoryViewModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        public string NewCategoryName
        {
            get { return _newCategoryName; }
            set
            {
                _newCategoryName = value;
                OnPropertyChanged("NewCategoryName");
            }
        }

        public ICommand DeleteCategoryCommand
        {
            get
            {
                if (_deleteCategoryCommand == null)
                {
                    _deleteCategoryCommand = new RelayCommand(
                        param => DeleteCategory()
                        );
                }
                return _deleteCategoryCommand;
            }
        }

        private void DeleteCategory()
        {
            if (SelectedCategory != null)
            {
                _transManager.DeleteCategory(SelectedCategory.CategoryId);
                Categories.Remove(SelectedCategory);
                _transManager.UpdateCategories();
                Mediator.Instance.NotifyListeners(ViewModelMessages.CategoriesChanged, "CategoryDeleted");
            }
        }

        public ICommand AddNewCategoryCommand
        {
            get
            {
                if (_addNewCategoryNameCommand == null)
                {
                    _addNewCategoryNameCommand = new RelayCommand(
                        param => AddNewCategoryName(), param => AddNewCategoryNameCommandCanExecute()
                        );
                }
                return _addNewCategoryNameCommand;
            }
        }

        private void AddNewCategoryName()
        {
            var temp = _transManager.TransactionCategories.Where(n => n.Name == NewCategoryName);

            if (temp.Any())
            {
                _windowFactory.ShowMessage("A Category with this name already exists!");
                NewCategoryName = "";
            }
            else
            {
                _transManager.AddCategory(NewCategoryName);
                Init();
                Mediator.Instance.NotifyListeners(ViewModelMessages.CategoriesChanged, "CategoryAdded");
                NewCategoryName = "";
            }
        }

        private bool AddNewCategoryNameCommandCanExecute()
        {
            if (!String.IsNullOrEmpty(NewCategoryName))
            {
                return true;
            }

            return false;
        }
    }
}
