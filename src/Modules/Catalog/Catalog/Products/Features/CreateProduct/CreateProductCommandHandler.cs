using Shared.CQRS;

namespace Catalog.Products.Features.Create_Product
{
    public record CreateProductCommand(string Name, string Description, decimal Price, string ImageFile, List<string> Category) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
