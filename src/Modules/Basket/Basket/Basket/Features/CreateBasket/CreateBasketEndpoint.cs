namespace Basket.Basket.Features.CreateBasket
{
    public record CreateBasketRequest(ShoppingCartDTO ShoppingCart);
    public record CreateBasketResponse(Guid Id);
    public class CreateBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (CreateBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateBasketCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateBasketResponse>();
                return Results.Created($"/basket/{response.Id}", response);
            })
            .Produces<CreateBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Create Basket")
            .WithSummary("Create Basket")
            .RequireAuthorization();
        }
    }
}
