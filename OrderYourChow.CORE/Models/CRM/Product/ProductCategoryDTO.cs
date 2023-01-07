using OrderYourChow.CORE.Models.Shared.Error;

namespace OrderYourChow.CORE.Models.CRM.Product
{
    public class ProductCategoryDTO
    {
        public int? ProductCategoryId { get; set; }
        public string Name { get; set; }
    }

    public sealed class EmptyProductCategoryDTO : ProductCategoryDTO, IErrorOperationDto
    {
        public EmptyProductCategoryDTO()
        {
        }
        public EmptyProductCategoryDTO(string message)
        {
            Message = message;
        }

        public string Message { get; }

    }

    public sealed class ErrorProductCategoryDTO : ProductCategoryDTO, IErrorOperationDto
    {
        public ErrorProductCategoryDTO(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }

    public sealed class DeletedProductCategoryDTO : ProductCategoryDTO
    {
    }
    public sealed class UpdatedProductCategoryDTO : ProductCategoryDTO
    {
    }
    public sealed class CreatedProductCategoryDTO : ProductCategoryDTO
    {
    }
}
