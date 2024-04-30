using Moq;
using MyRecipeBook.Domain;

namespace CommonTestUtilities;

public class UnitOfWorkBuilder
{
    public static IUnitOfWork Build()
    {
        var mock = new Mock<IUnitOfWork>();
        return mock.Object;
    }
}
