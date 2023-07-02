namespace Persistence;
public class Order
{
    public int OrderID { get; set; }
    public int CustomerID { get; set; }
    public int SellerID { get; set; }
    public int AccountantID { get; set; }
    public List<Phone> ListPhone { get; set; } = new List<Phone>();
    public string PaymentMethod { get; set; } = "default";
    public DateTime? OrderDate { get; set; }
    public string OrderNote { get; set; } = "default";
    public string? OrderStatus { get; set; }
    public decimal TotalDue { get; set; }
}