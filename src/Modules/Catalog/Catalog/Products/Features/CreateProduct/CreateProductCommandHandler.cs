namespace Catalog.Products.Features.Create_Product
{
    public record CreateProductCommand(ProductDTO Product) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(x => x.Product.ImageFile).NotEmpty().WithMessage("Image file is required.");
            RuleFor(x => x.Product.Category).NotEmpty().WithMessage("Category is required.");
        }
    }

    public class CreateProductCommandHandler(CatalogDbContext dbContext,
        //IValidator<CreateProductCommand> validator,
        ILogger<CreateProductCommandHandler> logger
        ) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //var validationResult = await validator.ValidateAsync(command, cancellationToken);
            //var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            //if (errors.Any()) { 
            //    throw new ValidationException(errors.FirstOrDefault());
            //}

            logger.LogInformation("CreateProductCommandHandler called with {@command}", command);

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
