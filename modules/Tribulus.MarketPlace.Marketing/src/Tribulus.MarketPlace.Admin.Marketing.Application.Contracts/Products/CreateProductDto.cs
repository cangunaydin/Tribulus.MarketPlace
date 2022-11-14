using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;
using Tribulus.MarketPlace.Marketing.Products;

namespace Tribulus.MarketPlace.Admin.Marketing.Products
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(ProductConsts.MaxNameLength,MinimumLength =ProductConsts.MinNameLength)]
        public string Name { get; set; }

        [CanBeNull]
        [StringLength(ProductConsts.MaxDescriptionLength)]
        public string Description { get; set; }
    }
}
