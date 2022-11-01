namespace Tribulus.MarketPlace.Admin.Products.Models
{
    public interface ProductMarketingCompleted : ProductLineCompleted
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
