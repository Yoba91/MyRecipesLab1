using System.Text;
using MyRecipesLab1.BLL.Enums;
using MyRecipesLab1.DAL.Models;

namespace MyRecipesLab1.BLL.Helpers
{
    public static class RecipeExtensions
    {
        public static string AggregateIngredients(this RecipeDbo recipe)
        {
            StringBuilder result = new StringBuilder();
            foreach (var item in recipe.Ingredients)
            {
                result.Append(string.Format("{0} {1} {2}\n", item.Name, item.Weight, (TypeOfWeightEnum)item.TypeOfWeight));
            }
            return result.ToString();
        }
    }
}
