namespace Catalog.Data.Seed
{
    public static class InitialData
    {
        public static IEnumerable<Product> Products => new List<Product>
        {
            Product.Create(new Guid("11111111-1111-1111-1111-111111111111"), "Product 1", "Description for Product 1", 10.99m, "product1.jpg", new List<string> { "Category A", "Category B" }),
            Product.Create(new Guid("22222222-2222-2222-2222-222222222222"), "Product 2", "Description for Product 2", 20.99m, "product2.jpg", new List<string> { "Category C" }),
            Product.Create(new Guid("33333333-3333-3333-3333-333333333333"), "Product 3", "Description for Product 3", 30.99m, "product3.jpg", new List<string> { "Category A" }),
            Product.Create(new Guid("44444444-4444-4444-4444-444444444444"), "Product 4", "Description for Product 4", 40.99m, "product4.jpg", new List<string> { "Category B" })
        };
    }
}
