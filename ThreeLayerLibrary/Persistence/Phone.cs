namespace Persistence;

public class Phone
{
    public int PhoneID { get; set; } = 0;
    public string PhoneName { get; set; } = "default";
    public string Brand { get; set; } = "default";
    public string? CPU { get; set; }
    public string? RAM { get; set; }
    public decimal DiscountPrice { get; set; }
    public decimal Price { get; set; }
    public string? BatteryCapacity { get; set; }
    public string OS { get; set; } = "default";
    public string? SimSlot { get; set; }
    public string? ScreenHz { get; set; }
    public string? ScreenResolution { get; set; }
    public string? ROM { get; set; }
    public string? StorageMemory { get; set; }
    public string? MobileNetwork { get; set; }
    public int Quantity { get; set; } = 1;
    public string? PhoneSize { get; set; }

}