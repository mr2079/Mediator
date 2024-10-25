using Mediator.BuildingBlocks;

namespace Mediator.ConsoleApp.Products;

public record AddProductCommand(
    string Title,
    int Price) : IRequest<Guid>;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
{
    public Task<Guid> Handle(AddProductCommand command)
    {
        return Task.FromResult(Guid.NewGuid());
    }
}