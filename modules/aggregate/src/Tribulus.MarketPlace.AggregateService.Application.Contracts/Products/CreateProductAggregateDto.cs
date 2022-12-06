namespace Tribulus.MarketPlace.AggregateService.Products;

public class CreateProductAggregateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockCount { get; set; }
}
