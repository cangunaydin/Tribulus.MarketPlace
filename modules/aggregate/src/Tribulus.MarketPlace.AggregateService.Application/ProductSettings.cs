namespace Tribulus.MarketPlace.AggregateService
{
    public class ProductSettings
    {
        public string ConnectionString { get; set; }

        public int GracePeriodTime { get; set; }

        public bool SendConfirmationEmail { get; set; }
    }
}
