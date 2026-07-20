namespace Basket.Basket.Features.CreateBasket
{
    public record CreateBasketCommand(ShoppingCartDTO ShoppingCart) : ICommand<CreateBasketResult>;
    public record CreateBasketResult(Guid Id);

    public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
    {
        public CreateBasketCommandValidator()
        {
            RuleFor(x => x.ShoppingCart.UserName).NotNull().WithMessage("User name is required.");
        }
    }
    public class CreateBasketHandler(BasketDbContext dbContext) : ICommandHandler<CreateBasketCommand, CreateBasketResult>
    {
        public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
        {
            var shoppingCart = CreateNewBasket(command.ShoppingCart);

            dbContext.ShoppingCarts.Add(shoppingCart);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateBasketResult(shoppingCart.Id);
        }

        private ShoppingCart CreateNewBasket(ShoppingCartDTO shoppingCartDTO)
        {
            var shoppingCart = ShoppingCart.Create(Guid.NewGuid(), shoppingCartDTO.UserName);

            shoppingCartDTO.Items.ForEach(item =>
            {
                shoppingCart.AddItem(item.ProductId, item.Quantity, item.Color, item.Price, item.ProductName);
            });

            return shoppingCart;
        }
    }
}
