namespace Tribulus.MarketPlace.Admin.Models
{
    public interface ProductMarketingCompleted : FutureCompleted
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
