using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyRecipesLab1.PAL.ViewModels;
using Xamarin.Forms;

namespace MyRecipesLab1
{
    public partial class MainPage : CarouselPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new RecipeListViewModel() { Navigation = this.Navigation };
        }
    }
}
