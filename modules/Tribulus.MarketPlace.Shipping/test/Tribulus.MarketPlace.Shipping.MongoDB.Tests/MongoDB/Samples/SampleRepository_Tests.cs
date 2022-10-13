using Tribulus.MarketPlace.Shipping.Samples;
using Xunit;

namespace Tribulus.MarketPlace.Shipping.MongoDB.Samples;

[Collection(MongoTestCollection.Name)]
public class SampleRepository_Tests : SampleRepository_Tests<ShippingMongoDbTestModule>
{
    /* Don't write custom repository tests here, instead write to
     * the base class.
     * One exception can be some specific tests related to MongoDB.
     */
}
