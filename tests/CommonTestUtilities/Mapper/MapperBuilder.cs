using AutoMapper;

namespace CommonTestUtilities;

public class MapperBuilder
{
    public static IMapper Build()
    {
        return new MapperConfiguration(opt =>
        {
            opt.AddProfile(new MyRecipeBook.Application.Services.AutoMapping());
        }).CreateMapper();
    }
}
