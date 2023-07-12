using Persistence;
using MySqlConnector;
using System.Collections.Generic;
namespace DAL;

public class DiscountByTotalDue : DiscountPolicy
{
    public DiscountPolicyDAL dpDal = new DiscountPolicyDAL();
    public Order Order { get; set; }
    public DiscountByTotalDue(string policyName, Order order, DateTime fromdate, DateTime todate)
    {
        this.PolicyName = policyName;
        this.Order = order;
        this.FromDate = fromDate;
        this.ToDate = toDate;
        this.Description = description;
    }
    public override decimal Discount(Order order)
    {
        decimal totalDiscount = 0;
        if (IsEligible(order))
        {
            return totalDiscount = order.TotalDue;
        }
        return totalDiscount;
    }
    public override bool IsEligible(Order order)
    {
        if (order.OrderCustomer.Job == this.Job) return true;
        return false;
    }
}
public class DiscountByProducts : DiscountPolicy
{
    List<Phone> PhonesHaveDiscount { get; set; }
    public DiscountByProducts(string policyName, Order order, DateTime fromdate, DateTime todate)
    {
        PhoneBL phoneBL = new PhoneBL();
        this.PolicyName = policyName;
        this.Order = order;
        this.FromDate = fromdate;
        this.ToDate = todate;
        this.PhonesHaveDiscount = phoneBL.PhonesHaveDiscount();
    }
    public override decimal Discount(Order order)
    {
        decimal totalDiscount = 0;
        if (IsEligible(order))
        {
            foreach (Phone phone in order.ListPhone)
                totalDiscount += phone.DiscountPrice;
            return totalDiscount;
        }
        return totalDiscount;
    }
    public override bool IsEligible(Order order) { return true; }
}