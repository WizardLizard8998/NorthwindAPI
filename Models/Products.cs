namespace NorthwindAPI.Models
{
    public class Products
    {
        public int ProductId { get; set; }
        public string ProductName{ get; set; }
        public int SupplierID{ get; set; }
        public int CategoryID{ get; set; }
        public string QuantityPerUnit{ get; set; }
        public decimal UnitPrice{ get; set; }
        public int UnitInStock{ get; set; }
        public int UnitsOnOrder{ get; set; }
        public int ReorderLevel{ get; set; }
        public byte Discontinued{ get; set; }
    }
}
