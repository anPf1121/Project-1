namespace Persistence;
public class Order
{
    public int OrderID { get; set; }
    public Customer OrderCustomer { get; set; } = new Customer();
    public Staff OrderSeller { get; set; } = new Staff();
    public Staff OrderAccountant { get; set; } = new Staff();
    public List<Phone> ListPhone { get; set; } = new List<Phone>();
    public string PaymentMethod { get; set; } = "default";
    public DateTime? OrderDate { get; set; }
    public string OrderNote { get; set; } = "default";
    public string? OrderStatus { get; set; }
    public decimal TotalDue { get; set; }
    public int? DiscountPolicyID { get; set; }
    public Dictionary<string, Phone> PhoneWithImei{get;set;} = new Dictionary<string, Phone>();
    public string? DiscountPolicyID { get; set; }
}