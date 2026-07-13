namespace Catalog.Products.Features.GetProducts
{
    public record GetProductsQuery() : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<ProductDTO> Products);
    public class GetProductsHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await dbContext.Products.AsNoTracking().OrderBy(p => p.Name).ToListAsync(cancellationToken);

            //var productsDtos = ProjectToProductDTO(products);
            var productsDtos = products.Adapt<List<ProductDTO>>();
            return new GetProductsResult(productsDtos);
        }

        //private IEnumerable<ProductDTO> ProjectToProductDTO(List<Product> products)
        //{
        //    return products.Select(p => new ProductDTO(
        //        p.Id,
        //        p.Name,
        //        p.Description,
        //        p.Price,
        //        p.ImageFile,
        //        p.Category
        //    )).ToList();
        //}
    }
}
