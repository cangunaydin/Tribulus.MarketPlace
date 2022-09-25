using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using System;
using System.Threading.Tasks;
using Tribulus.MarketPlace.Products;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using Xunit;

namespace Tribulus.MarketPlace.Admin.Products;

public class ProductAppServiceTests:MarketPlaceAdminApplicationTestBase
{
    private readonly IProductAppService _productAppService;
    private ICurrentUser _currentUser;
    private readonly MarketPlaceTestData _marketPlaceTestData;
    private readonly IRepository<Product, Guid> _productRepository;

    public ProductAppServiceTests()
    {
        _productAppService=GetRequiredService<IProductAppService>();
        _currentUser=GetRequiredService<ICurrentUser>();
        _marketPlaceTestData=GetRequiredService<MarketPlaceTestData>();
        _productRepository=GetRequiredService<IRepository<Product, Guid>>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }
    [Fact]
    public async Task Should_Create_New_Valid_Product()
    {
        Login(_marketPlaceTestData.UserAdminId);

        var productDto = await _productAppService.CreateAsync(new CreateProductDto()
        {
            Description = "New Product Description",
            Name="New Product Name",
            Price=100,
            StockCount=10
        });
        productDto.Description.ShouldBe("New Product Description");
        productDto.Name.ShouldBe("New Product Name");
        productDto.Price.ShouldBe(100);
        productDto.StockCount.ShouldBe(10);
    }

    [Fact]
    public async Task Should_Update_Product()
    {
        Login(_marketPlaceTestData.UserAdminId);
        await _productAppService.UpdateAsync(_marketPlaceTestData.ProductIphone13Id, new UpdateProductDto()
        {
            Name = "Updated Iphone 13 Name",
            Description="Updated Iphone 13 Description",
            Price=120,
            StockCount=20
        });
        var updatedProduct = await GetProductOrNullAsync(_marketPlaceTestData.ProductIphone13Id);
        updatedProduct.Name.ShouldBe("Updated Iphone 13 Name");
        updatedProduct.Description.ShouldBe("Updated Iphone 13 Description");
        updatedProduct.Price.ShouldBe(120);
        updatedProduct.StockCount.ShouldBe(20);
    }

    [Fact]
    public async Task Should_Get_Valid_Product()
    {
        Login(_marketPlaceTestData.UserAdminId);
        var product=await _productAppService.GetAsync(_marketPlaceTestData.ProductIphone13Id);
        product.ShouldNotBeNull();
        product.Name.ShouldBe(_marketPlaceTestData.ProductIphone13Name);
        
    }

    [Fact]
    public async Task Should_Filter_Products_By_Name()
    {
        Login(_marketPlaceTestData.UserAdminId);
        string searchName = "iphone";
        var productsResult = await _productAppService.GetListAsync(new ProductListFilterDto() { Name = searchName });
        foreach (var product in productsResult.Items)
        {
            product.Name.ShouldContain(searchName);
        }
    }

    private async Task<Product> GetProductOrNullAsync(Guid productId)
    {
        return await WithUnitOfWorkAsync(async () =>
        {
            return await _productRepository.FirstOrDefaultAsync(
                x => x.Id== productId
            );
        });
    }
    private void Login(Guid userId)
    {
        _currentUser.Id.Returns(userId);
        _currentUser.IsAuthenticated.Returns(true);
    }
}
