using OrderYourChow.CORE.Models.Shared.Error;

namespace OrderYourChow.CORE.Models.CRM.Product
{
    public class ProductDTO : AddProductDTO
    {
        public int ProductId { get; set; }
        public string ProductCategory { get; set; }
    }
    public sealed class EmptyProductDTO : ProductDTO, IErrorOperationDto
    {
        public EmptyProductDTO(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }

    public sealed class ErrorProductDTO : ProductDTO, IErrorOperationDto
    {
        public ErrorProductDTO(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }

    public sealed class DeletedProductDTO : ProductDTO
    {
    }
    public sealed class UpdatedProductDTO : ProductDTO
    {
    }
    public sealed class CreatedProductDTO : ProductDTO
    {
    }
}
