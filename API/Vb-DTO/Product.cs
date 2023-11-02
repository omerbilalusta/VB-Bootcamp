namespace Vb_DTO
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public decimal TaxRate { get; set; }
    }

    public class ProductRequest2
    {
        public int StockQuantity { get; set; }
    }

    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public decimal TaxRate { get; set; }

        public int CompanyId { get; set; }
        public virtual CompanyResponseShort Company { get; set; }
    }
}
