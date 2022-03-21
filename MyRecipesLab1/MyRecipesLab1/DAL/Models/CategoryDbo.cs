using MyRecipesLab1.Core;
using Realms;

namespace MyRecipesLab1.DAL.Models
{
    public class CategoryDbo : RealmObject
    {
        [PrimaryKey]
        public long Id { get; set; } = Generator.GenerateId();

        public string Name { get; set; }
    }
}
