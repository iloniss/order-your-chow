namespace OrderYourChow.CORE.Queries.CRM.Product
{
    public class GetProductCategoryQuery
    {
        public GetProductCategoryQuery(string name)
        {
            Name = name;
        }
        public GetProductCategoryQuery(int? productCategoryId)
        {
            ProductCategoryId = productCategoryId;
        }

        public string Name { get; }
        public int? ProductCategoryId { get; }
    }
}
