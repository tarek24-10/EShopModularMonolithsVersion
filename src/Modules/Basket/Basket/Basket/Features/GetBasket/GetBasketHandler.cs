namespace Basket.Basket.Features.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCartDTO ShoppingCart);
    public class GetBasketHandler(BasketDbContext dbContext) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery command, CancellationToken cancellationToken)
        {
            var basket = await dbContext.ShoppingCarts.AsNoTracking()
                .Include(b => b.Items)
                .SingleOrDefaultAsync(b => b.UserName == command.UserName, cancellationToken);

            if (basket == null)
            {
                throw new BasketNotFoundException(command.UserName);
            }

            var basketDTO = basket.Adapt<ShoppingCartDTO>();

            return new GetBasketResult(basketDTO);
        }
    }
}
