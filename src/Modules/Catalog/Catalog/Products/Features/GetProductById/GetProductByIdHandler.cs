namespace Catalog.Products.Features.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(ProductDTO Product);
    public class GetProductByIdHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken: cancellationToken);

            if (product == null)
                throw new Exception($"Product not found {query.Id}");

            var productDTO = product.Adapt<ProductDTO>();

            return new GetProductByIdResult(productDTO);
        }
    }
}
