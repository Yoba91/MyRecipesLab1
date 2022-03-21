using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MyRecipesLab1.BLL.Helpers;
using MyRecipesLab1.DAL;
using MyRecipesLab1.DAL.Models;
using Xamarin.Forms;

namespace MyRecipesLab1.PAL.ViewModels
{
    public class RecipeViewModel : ViewModelBase
    {
        private RecipeDbo _dataModel;
        private readonly RealmRepository<RecipeDbo> _recipeRepository;
        private readonly RealmRepository<IngredientDbo> _ingredientRepository;
        private ObservableCollection<IngredientViewModel> _ingredients;
        private RecipeListViewModel _recipeListViewModel;
        private IngredientViewModel _selectedIngredient;
        private bool _isNewRecipe = true;

        public RecipeViewModel(RecipeDbo recipe)
        {
            _recipeRepository = new RealmRepository<RecipeDbo>();
            _ingredientRepository = new RealmRepository<IngredientDbo>();
            _dataModel = recipe;
            _ingredients = new ObservableCollection<IngredientViewModel>();
            foreach (var item in _dataModel.Ingredients)
            {
                _ingredients.Add(new IngredientViewModel(item));
            }
            AddIngredientCommand = new Command(AddIngredient);
            RemoveIngredientCommand = new Command(RemoveIngredient);
        }

        public ICommand AddIngredientCommand { get; protected set; }
        public ICommand RemoveIngredientCommand { get; protected set; }

        public RecipeListViewModel ListViewModel
        {
            get { return _recipeListViewModel; }
            set
            {
                if (_recipeListViewModel != value)
                {
                    _recipeListViewModel = value;
                    OnPropertyChanged(nameof(ListViewModel));
                }
            }
        }

        public IngredientViewModel SelectedIngredient
        {
            get { return _selectedIngredient; }
            set { _selectedIngredient = value; OnPropertyChanged(nameof(SelectedIngredient)); }
        }

        public string Name
        {
            get { return _dataModel.Name; }
            set { _dataModel.Name = value; OnPropertyChanged(nameof(Name)); }
        }

        public int Category
        {
            get
            {
                return _dataModel.Category;
            }
            set { _dataModel.Category = value; OnPropertyChanged(nameof(Category)); }
        }

        public List<string> Categories => ListViewModel.Categories.Where(x => ListViewModel.Categories.IndexOf(x) != 0).ToList();

        public string CategoryName => ListViewModel.Categories[Category];

        public ObservableCollection<IngredientViewModel> Ingredients
        {
            get { return _ingredients; }
            set { _ingredients = value; OnPropertyChanged(nameof(Ingredients)); }
        }

        public string AddEditLabel
        {
            get
            {
                _isNewRecipe = _recipeRepository.GetById(_dataModel.Id) is null;
                if (_isNewRecipe)
                {
                    return "Добавить";
                }
                return "Изменить";
            }
        }

        public string IngredientsList => _dataModel.AggregateIngredients();

        public bool IsValid
        {
            get
            {
                return true;
            }
        }

        private void AddIngredient()
        {
            var dbo = new IngredientDbo();
            var vm = new IngredientViewModel(dbo);
            _dataModel.Ingredients.Add(dbo);
            Ingredients.Add(vm);
        }

        private void RemoveIngredient()
        {
            if (Ingredients.Count > 0)
            {
                Ingredients.RemoveAt(Ingredients.Count - 1);
                _dataModel.Ingredients.RemoveAt(_dataModel.Ingredients.Count - 1);
            }
        }

        public RecipeDbo Save()
        {
            RemoveData();
            if (string.IsNullOrEmpty(_dataModel.Name))
            {
                _dataModel.Name = _dataModel.CreateDate.ToString("dd-MM-yy hh_mm");
            }
            return _recipeRepository.Save(_dataModel);
        }

        public void Remove()
        {
            RemoveData();
        }

        private void RemoveData()
        {
            foreach (var item in _dataModel.Ingredients)
            {
                _ingredientRepository.RemoveById(item.Id);
            }
            _recipeRepository.RemoveById(_dataModel.Id);
        }
    }
}
