using Persistence;
using MySqlConnector;
using System.Collections.Generic;
namespace DAL;

public class DiscountByPolicy
{
    public string Title { get; set; } = "default";
    public Order Order { get; set; } = default!;
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public virtual decimal Discount(Order order) { return 0; }
    public virtual bool IsTrigger(Order order) { return false; }
}
public class DiscountByTotalDue : DiscountByPolicy
{
    public decimal DiscountPrice { get; set; }
    public decimal TotalDueCondition { get; set; }
    public DiscountByTotalDue(string title, Order order, DateTime fromdate, DateTime todate, decimal discountprice, decimal condition)
    {
        this.Title = title;
        this.Order = order;
        this.FromDate = fromdate;
        this.ToDate = todate;
        this.DiscountPrice = discountprice;
        this.TotalDueCondition = condition;
    }
    public override decimal Discount(Order order)
    {
        if (IsTrigger(order)) return DiscountPrice;
        else return 0;
    }
    public override bool IsTrigger(Order order)
    {
        if (order.TotalDue >= TotalDueCondition) return true;
        else return false;
    }
}
public class DiscountByPhone : DiscountByPolicy
{
    public DiscountByPhone(string title, Order order, DateTime fromdate, DateTime todate)
    {
        this.Title = title;
        this.Order = order;
        this.FromDate = fromdate;
        this.ToDate = todate;
    }
    public override decimal Discount(Order order)
    {
        if (IsTrigger(order) == true)
        {
            decimal sumAllDiscount = 0;
            foreach (var phone in order.ListPhone)
            {
                sumAllDiscount += phone.DiscountPrice;
            }
            return sumAllDiscount;
        }
        else return 0;
    }
    public override bool IsTrigger(Order order)
    {
        int countPhoneHaveDiscount = 0;
        foreach (var phone in order.ListPhone)
        {
            if (phone.DiscountPrice != 0) countPhoneHaveDiscount++;
        }
        if (countPhoneHaveDiscount != 0) return true;
        else
        {
            return false;
        }
    }
}
public class DiscountConfirmation
{
    public Order Order { get; set; }
    public List<DiscountByPolicy> ListDiscount;
    public DiscountConfirmation(Order order, List<DiscountByPolicy> listdiscount)
    {
        this.Order = order;
        this.ListDiscount = listdiscount;
    }
    public List<DiscountByPolicy> GetDiscountConfirmed()
    {
        List<DiscountByPolicy> output = new List<DiscountByPolicy>();
        int count = this.ListDiscount.Count();
        int leftCompare;
        int rightCompare;
        DateTime currentTime = DateTime.Now;
        for (int i = 0; i < count; i++)
        {
            leftCompare = DateTime.Compare(currentTime, ListDiscount[i].FromDate);
            rightCompare = DateTime.Compare(currentTime, ListDiscount[i].ToDate);
            if (leftCompare >= 0 && rightCompare <= 0) output.Add(ListDiscount[i]);
        }
        return output;
    }
    public List<DiscountByPolicy> GetDiscountConfirmToOrder(Order order)
    {
        List<DiscountByPolicy> output = new List<DiscountByPolicy>();
        List<DiscountByPolicy> Validated = GetDiscountConfirmed();
        int count = Validated.Count();
        if (count > 0)
        {
            foreach (var discount in Validated)
                if (discount.IsTrigger(order)) output.Add(discount);
        }
        return output;
    }
    public decimal GetTotalDueAfterDiscount(Order order)
    {
        decimal sum = 0;
        foreach (var discount in GetDiscountConfirmToOrder(order))
        {
            sum += discount.Discount(order);
        }
        order.TotalDue -= sum;
        return order.TotalDue;
    }
}
