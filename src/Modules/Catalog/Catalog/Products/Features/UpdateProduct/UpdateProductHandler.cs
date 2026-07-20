namespace Catalog.Products.Features.UpdateProduct
{
    public record UpdateProductCommand(ProductDTO Product) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Product.Id).NotEmpty().WithMessage("ID is required.");
            RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(x => x.Product.ImageFile).NotEmpty().WithMessage("Image file is required.");
            RuleFor(x => x.Product.Category).NotEmpty().WithMessage("Category is required.");
        }
    }
    public class UpdateProductHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FindAsync([command.Product.Id], cancellationToken: cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException(command.Product.Id);
            }

            UpdateProductWithNewValues(product, command.Product);
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }

        private void UpdateProductWithNewValues(Product product, ProductDTO productDto)
        {
            product.Update(
                productDto.Name,
                productDto.Description,
                productDto.Price,
                productDto.ImageFile,
                productDto.Category
            );
        }
    }
}
