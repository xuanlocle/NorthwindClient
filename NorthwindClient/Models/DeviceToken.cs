namespace NorthwindClient.Models;

public class DeviceToken
{
    
    public int DeviceTokenId { get; set; }
    public string Token { get; set; }
    public DateTime RegisteredAt { get; set; }
}