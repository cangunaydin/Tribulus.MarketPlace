using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Tribulus.MarketPlace.Shipping.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
