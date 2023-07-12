namespace Persistence;
public class DiscountPolicy
{
    public int PolicyID { get; set; }
    public string PolicyName { get; set; } = default!;
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public Decimal DiscountPrice { get; set; } = default!;
    public string Description { get; set; } = default!;
    public virtual decimal Discount(Order order) { return 10; }
    public virtual bool IsEligible(Order order) { return false; }
}