namespace Catalog.Products.Features.GetProductByCategory
{
    //public record GetProductByCategoryRequest(string Category);
    public record GetProductByCategoryResponse(IEnumerable<ProductDTO> Products);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category));
                var response = result.Adapt<GetProductByCategoryResponse>();
                return Results.Ok(response);
            })
                .WithName("Get Product by Category")
                .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product by Category")
                .WithDescription("Get Product by Category");
        }
    }
}
