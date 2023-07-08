namespace Persistence;
public class Customer
{
    public int? CustomerID { get; set; }
    public string CustomerName { get; set; } = "default";
    public string? Job { get; set; }
    public string PhoneNumber { get; set; } = "default";
    public string? Address { get; set; }
    public Customer(int? customerID, string customerName, string? job, string phoneNumber, string? address) {
        CustomerID = customerID;
        CustomerName = customerName;
        Job = job;
        PhoneNumber = phoneNumber;
        Address = address;
    }
}