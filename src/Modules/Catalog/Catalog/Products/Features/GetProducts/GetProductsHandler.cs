using Shared.Pagination;

namespace Catalog.Products.Features.GetProducts
{
    public record GetProductsQuery(PaginationRequest PaginationRequest) : IQuery<GetProductsResult>;
    public record GetProductsResult(PaginatedResult<ProductDTO> Products);
    public class GetProductsHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            //var products = await dbContext.Products.AsNoTracking().OrderBy(p => p.Name).ToListAsync(cancellationToken);

            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbContext.Products.CountAsync(cancellationToken);

            var products = await dbContext.Products
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            //var productsDtos = ProjectToProductDTO(products);
            var productsDtos = products.Adapt<List<ProductDTO>>();

            return new GetProductsResult(new PaginatedResult<ProductDTO>(pageIndex, pageSize, totalCount, productsDtos));
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
