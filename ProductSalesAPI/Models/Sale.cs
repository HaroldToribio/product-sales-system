namespace ProductSalesAPI.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public List<SaleProduct> SaleProducts { get; set; } = new List<SaleProduct>();
        public decimal Total { get; set; }
    }
}