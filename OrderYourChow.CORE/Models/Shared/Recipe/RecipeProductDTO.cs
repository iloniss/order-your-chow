using OrderYourChow.CORE.Enums;

namespace OrderYourChow.CORE.Models.Shared.Recipe
{
    public class RecipeProductDTO
    {
        public int? RecipeProductId { get; set; }
        public int ProductId { get; set; }
        public int ProductMeasureId { get; set; }
        public decimal Count { get; set; }
        public  RecipeProductStatus? Status { get; set; }
    }
}
