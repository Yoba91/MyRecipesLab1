using AutoMapper;
using MyRecipesLab1.DAL.Models;

namespace MyRecipesLab1.Core.Config.AutoMapper
{
    public class RealmModelsProfile : Profile
    {
        public RealmModelsProfile()
        {
            CreateMap<RecipeDbo, RecipeDbo>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Ingredients, y => y.MapFrom(z => z.Ingredients))
                .ForMember(x => x.CreateDate, y => y.MapFrom(z => z.CreateDate))
                .ForMember(x => x.Category, y => y.MapFrom(z => z.Category))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<IngredientDbo, IngredientDbo>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Weight, y => y.MapFrom(z => z.Weight))
                .ForMember(x => x.TypeOfWeight, y => y.MapFrom(z => z.TypeOfWeight))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<CategoryDbo, CategoryDbo>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
