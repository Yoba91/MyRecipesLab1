using System;
using System.Collections.Generic;
using MyRecipesLab1.Core;
using Realms;

namespace MyRecipesLab1.DAL.Models
{
    public class RecipeDbo : RealmObject
    {
        [PrimaryKey]
        public long Id { get; set; } = Generator.GenerateId();

        public string Name { get; set; }

        public IList<IngredientDbo> Ingredients { get; }

        public int Category { get; set; } = 0;

        public DateTimeOffset CreateDate { get; set; } = DateTime.Now;
    }
}
