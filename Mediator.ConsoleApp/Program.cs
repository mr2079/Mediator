using Mediator.ConsoleApp.Products;

var mediator = new Mediator.ConsoleApp.Mediator();

var command = new AddProductCommand(
    "Apple iphone 13 pro max",
    1099);

var productId = await mediator.Send<AddProductCommand, Guid>(command);

Console.WriteLine(productId);

var query = new GetProductQuery(
    Guid.NewGuid());

var productTitle = await mediator.Send<GetProductQuery, string>(query);

Console.WriteLine(productTitle);

Console.ReadKey();