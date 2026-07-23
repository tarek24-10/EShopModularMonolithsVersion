namespace Basket.Basket.Features.RemoveItemFromBasket
{
    public record RemoveItemFromBasketResponse(Guid Id);
    public class RemoveItemFromBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}/items/{itemId}", async ([FromRoute] string userName, [FromRoute] Guid itemId, ISender sender) =>
            {
                var result = await sender.Send(new RemoveItemFromBasketCommand(userName, itemId));
                var response = result.Adapt<RemoveItemFromBasketResponse>();
                return Results.Ok(response);
            })
            .Produces<RemoveItemFromBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Remove Item From Basket")
            .WithSummary("Remove Item From Basket");
        }
    }
}
