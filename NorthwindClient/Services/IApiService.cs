using System.Net;
using NorthwindClient.Models;

namespace NorthwindClient.Services;

public interface IApiService
{
    Task<bool> RegisterDeviceTokenAsync(string token);

    Task<List<CustomerModel>> GetCustomersAsync();

    Task<CustomerModel?> GetCustomerByIdAsync(string id);

    Task<List<OrderModel>> GetOrdersByCustomerAsync(string customerId);

    Task<bool> AddOrderAsync(OrderModel order);

    Task<bool> DeleteCustomerAsync(string customerId);
}