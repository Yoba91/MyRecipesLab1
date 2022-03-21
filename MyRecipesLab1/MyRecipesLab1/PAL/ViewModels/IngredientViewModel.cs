using System.Collections.Generic;
using MyRecipesLab1.DAL.Models;

namespace MyRecipesLab1.PAL.ViewModels
{
    public class IngredientViewModel : ViewModelBase
    {
        private IngredientDbo _dataModel;
        private List<string> _typesOfWeight = new List<string> { "г.", "мл." };

        public IngredientViewModel(IngredientDbo dataModel)
        {
            _dataModel = dataModel;
        }

        public List<string> TypesOfWeight => _typesOfWeight;

        public int SelectedIndexOfTypeOfWeight
        {
            get => _dataModel.TypeOfWeight;
            set
            {
                _dataModel.TypeOfWeight = value;
                OnPropertyChanged(nameof(SelectedIndexOfTypeOfWeight));
            }
        }

        public string Name
        {
            get { return _dataModel.Name; }
            set { _dataModel.Name = value; OnPropertyChanged(nameof(Name)); }
        }

        public int Weight
        {
            get { return _dataModel.Weight; }
            set { _dataModel.Weight = value; OnPropertyChanged(nameof(Weight)); }
        }

    }
}
