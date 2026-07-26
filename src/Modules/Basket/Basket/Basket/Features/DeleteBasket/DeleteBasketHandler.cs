namespace Basket.Basket.Features.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);
    public class DeleteBasketHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            //var shoppingCart = await dbContext.ShoppingCarts.SingleOrDefaultAsync(x => x.UserName == command.UserName, cancellationToken);
            //if (shoppingCart == null)
            //{
            //    throw new BasketNotFoundException(command.UserName);
            //}
            //dbContext.ShoppingCarts.Remove(shoppingCart);
            //await dbContext.SaveChangesAsync(cancellationToken);

            await repository.DeleteBasket(command.UserName, cancellationToken);
            return new DeleteBasketResult(true);
        }
    }
}
