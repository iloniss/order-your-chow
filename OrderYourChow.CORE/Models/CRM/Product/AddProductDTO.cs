namespace OrderYourChow.CORE.Models.CRM.Product
{
    public class AddProductDTO
    {
        public int? ProductId { get; set; }
        public string Name { get; set; }
        public int ProductCategoryId { get; set; }
        public string Image { get; set; }
    }
    public sealed class EmptyAddProductDTO : AddProductDTO
    {

    }
    public sealed class ErrorAddProductDTO : AddProductDTO
    {

    }
}
