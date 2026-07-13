namespace Catalog.Products.Features.Create_Product
{
    public record CreateProductCommand(ProductDTO Product) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductCommandHandler(CatalogDbContext dbContext) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = CreateNewProduct(command.Product);
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
        }

        private Product CreateNewProduct(ProductDTO product)
        {
            return Product.Create(
                Guid.NewGuid(),
                product.Name,
                product.Description,
                product.Price,
                product.ImageFile,
                product.Category
            );
        }
    }
}
