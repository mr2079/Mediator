using Bogus;
using Mediator.BuildingBlocks;

namespace Mediator.ConsoleApp.Products;

public record GetProductQuery(
    Guid Id) : IRequest<string>;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, string>
{
    private readonly List<string> _products;

    public GetProductQueryHandler()
    {
        if (_products != null 
            && _products.Count != 0) return;

        _products = new();

        var faker = new Faker();

        for (int i = 0; i < 100; i++)
        {
            _products.Add(faker.Commerce.Product());
        }
    }

    public Task<string> Handle(GetProductQuery command)
    {
        var rnd = new Random();
        var index = rnd.Next(0, _products.Count);

        return Task.FromResult(_products[index]);
    }
}