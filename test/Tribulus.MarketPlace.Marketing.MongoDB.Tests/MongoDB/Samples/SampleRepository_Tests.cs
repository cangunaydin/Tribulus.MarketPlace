using Tribulus.MarketPlace.Marketing.Samples;
using Xunit;

namespace Tribulus.MarketPlace.Marketing.MongoDB.Samples;

[Collection(MongoTestCollection.Name)]
public class SampleRepository_Tests : SampleRepository_Tests<MarketingMongoDbTestModule>
{
    /* Don't write custom repository tests here, instead write to
     * the base class.
     * One exception can be some specific tests related to MongoDB.
     */
}
