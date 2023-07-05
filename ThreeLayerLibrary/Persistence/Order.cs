namespace Persistence;
public class Order
{
    public int OrderID { get; set; }
    public Customer OrderCustomer { get; set; } = default!;
    public Staff OrderSeller { get; set; } = default!;
    public Staff OrderAccountant { get; set; } = default!;
    public List<Phone> ListPhone { get; set; } = new List<Phone>();
    public string PaymentMethod { get; set; } = "default";
    public DateTime? OrderDate { get; set; }
    public string OrderNote { get; set; } = "default";
    public string? OrderStatus { get; set; }
    public decimal TotalDue { get; set; }
}