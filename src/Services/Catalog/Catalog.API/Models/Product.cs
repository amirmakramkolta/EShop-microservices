namespace Catalog.API.Models
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<string> Tags { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
    }
}
