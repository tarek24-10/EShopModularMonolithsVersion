namespace Catalog.Products.Models
{
    public class Product : Entity<Guid>
    {
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;

        public decimal Price { get; private set; }

        public string ImageFile { get; private set; } = default!;

        public List<string> Category { get; private set; } = new();

        public static Product Create(Guid id, string name, string description, decimal price, string imageFile, List<string> category)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var product = new Product()
            {
                Id = id,
                Name = name,
                Description = description,
                Price = price,
                ImageFile = imageFile,
                Category = category
            };

            return product;
        }

        public void Update(string name, string description, decimal price, string imageFile, List<string> category)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            Name = name;
            Description = description;
            Price = price;
            ImageFile = imageFile;
            Category = category;
        }
    }
}
