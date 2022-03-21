using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MyRecipesLab1.DAL;
using MyRecipesLab1.DAL.Models;
using MyRecipesLab1.PAL.Views;
using Xamarin.Forms;

namespace MyRecipesLab1.PAL.ViewModels
{
    public class RecipeListViewModel : ViewModelBase
    {
        private readonly RealmRepository<RecipeDbo> _recipeRepository;
        private ObservableCollection<RecipeViewModel> _recipes = new ObservableCollection<RecipeViewModel>();
        private RecipeViewModel _selectedRecipe;
        private readonly List<string> _categories = new List<string>() { "Все", "Завтрак", "Обед", "Ужин", "Десерт" };
        private int _selectedCategory = 0;
        private string _searchString;

        public RecipeListViewModel()
        {
            _recipeRepository = new RealmRepository<RecipeDbo>();
            var recipesDbo = _recipeRepository.GetAll();
            ApplySearch(recipesDbo);
            AddRecipeCommand = new Command(AddRecipe);
            SaveRecipeCommand = new Command(SaveRecipe);
            RemoveRecipeCommand = new Command(RemoveRecipe);
            BackCommand = new Command(Back);
        }

        public ICommand AddRecipeCommand { get; protected set; }
        public ICommand EditRecipeCommand { get; protected set; }
        public ICommand SaveRecipeCommand { get; protected set; }
        public ICommand RemoveRecipeCommand { get; protected set; }
        public ICommand BackCommand { get; protected set; }
        public ICommand ClearCommand { get; protected set; }

        public INavigation Navigation { get; set; }

        public RecipeViewModel SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                if (_selectedRecipe != value)
                {
                    var temp = value;
                    _selectedRecipe = null;
                    OnPropertyChanged(nameof(SelectedRecipe));
                    Navigation.PushAsync(new RecipeView(temp));
                }
            }
        }

        public int SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                var recipesDbo = GetRecipesList();
                ApplySearch(recipesDbo);
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                var recipesDbo = GetRecipesList();
                ApplySearch(recipesDbo);
                OnPropertyChanged(nameof(SearchString));
            }
        }

        public ObservableCollection<RecipeViewModel> RecipeList
        {
            get { return _recipes; }
            set { _recipes = value; OnPropertyChanged(nameof(RecipeList)); }
        }

        public List<string> Categories => _categories;

        private List<RecipeDbo> GetRecipesList()
        {
            List<RecipeDbo> result = new List<RecipeDbo>();
            if (string.IsNullOrEmpty(SearchString))
            {
                result = _recipeRepository.GetAll();
                if (SelectedCategory != 0)
                {
                    result = result.Where(x => x.Category == _selectedCategory).ToList();
                }
                return result;
            }
            result = _recipeRepository.FindByName(x => x.Name.Contains(SearchString));
            if (SelectedCategory != 0)
            {
                result = result.Where(x => x.Category == _selectedCategory).ToList();
            }
            return result;
        }

        private void ApplySearch(List<RecipeDbo> recipesDbo)
        {
            _recipes.Clear();
            foreach (var recipe in recipesDbo)
            {
                _recipes.Add(new RecipeViewModel(recipe) { ListViewModel = this });
            }
        }

        private void AddRecipe()
        {
            var dbo = new RecipeDbo();
            var vm = new RecipeViewModel(dbo) { ListViewModel = this };
            var view = new RecipeView(vm);
            Navigation.PushAsync(view);
        }

        private void Back()
        {
            Navigation.PopAsync();
        }

        private void SaveRecipe(object recipeObject)
        {
            RecipeViewModel recipe = recipeObject as RecipeViewModel;
            var dbo = recipe.Save();
            if (recipe != null && recipe.IsValid)
            {
                if (RecipeList.Contains(recipe))
                {
                    RecipeList.Remove(recipe);
                }
                RecipeList.Add(new RecipeViewModel(dbo) { ListViewModel = recipe.ListViewModel });
            }
            OnPropertyChanged(nameof(RecipeList));
            Back();
        }

        private void RemoveRecipe(object recipeObject)
        {
            RecipeViewModel recipe = recipeObject as RecipeViewModel;
            if (recipe != null)
            {
                RecipeList.Remove(recipe);
            }
            recipe.Remove();
            OnPropertyChanged(nameof(RecipeList));
            Back();
        }
    }
}
