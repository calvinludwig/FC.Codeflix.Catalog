using Bogus;

namespace FC.Codeflix.Catalog.IntegrationTests.Common;

public abstract class BaseFixture
{
    public Faker Faker { get; set; } = new("pt_BR");
}
