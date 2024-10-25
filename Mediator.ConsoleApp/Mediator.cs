using Mediator.BuildingBlocks;

namespace Mediator.ConsoleApp;

public interface IMediator
{
    Task<TResponse> Send<TRequest, TResponse>(TRequest request);
}

public class Mediator : IMediator
{
    private readonly Dictionary<Type, object> _handlers = new();

    public Mediator()
    {
        RegisterHandlers();
    }

    private void RegisterHandlers()
    {
        var assembly = typeof(Mediator).Assembly;

        var handlerInterfaceType = typeof(IRequestHandler<,>);

        var handlerTypes = assembly.GetTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType));

        foreach (var handlerType in handlerTypes)
        {
            var implementedInterface = handlerType.GetInterfaces()
                .First(t => t.IsGenericType && t.GetGenericTypeDefinition() == handlerInterfaceType);

            var requestType = implementedInterface.GetGenericArguments()[0];

            var handlerInstance = Activator.CreateInstance(handlerType);
            _handlers[requestType] = handlerInstance;
        }
    }

    public Task<TResponse> Send<TRequest, TResponse>(TRequest request)
    {
        if (!_handlers.TryGetValue(typeof(TRequest), out var handler)) 
            return null;

        return ((IRequestHandler<TRequest, TResponse>)handler).Handle(request);
    }
}