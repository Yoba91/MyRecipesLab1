using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyRecipesLab1.PAL.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyRecipesLab1.PAL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeView : ContentPage
    {
        public RecipeViewModel ViewModel { get; private set; }

        public RecipeView(RecipeViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            BindingContext = ViewModel;
        }
    }
}