namespace OrderYourChow.CORE.Models.CRM.Product
{
    public class ProductCategoryDTO
    {
        public int? ProductCategoryId { get; set; }
        public string Name { get; set; }
    }

    public sealed class EmptyProductCategoryDTO : ProductCategoryDTO
    {

    }

    public sealed class ErrorProductCategoryDTO : ProductCategoryDTO
    {

    }
}
