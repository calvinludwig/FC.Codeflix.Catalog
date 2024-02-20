using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.Common;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.Common;

public abstract class CategoryUseCasesBaseFixture : BaseFixture
{
    public Mock<ICategoryRepository> GetRepositoryMock()
    {
        return new Mock<ICategoryRepository>();
    }

    public Mock<IUnitOfWork> GetUnitOfWorkMock()
    {
        return new Mock<IUnitOfWork>();
    }

    public Catalog.Domain.Entity.Category GetExampleCategory()
    {
        return new Catalog.Domain.Entity.Category(
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            GetRandomBool()
        );
    }

    public string GetValidCategoryName()
    {
        var name = "";
        while (name.Length is < 3 or > 255)
            name = Faker.Commerce.Categories(1)[0];

        return name;
    }

    public string GetValidCategoryDescription()
    {
        var description = "";
        while (description.Length > 10_000 || description == "")
            description = Faker.Commerce.ProductDescription();

        return description;
    }

    public static bool GetRandomBool()
    {
        return new Random().NextDouble() < 0.5;
    }
}
