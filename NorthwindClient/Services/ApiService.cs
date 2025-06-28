using System.Net;
using System.Net.Http.Json;
using NorthwindClient.Models;

namespace NorthwindClient.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Fetch customers
    public async Task<List<CustomerModel>> GetCustomersAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<CustomerModel>>("customers");
        return response ?? new List<CustomerModel>();
    }

    public async Task<CustomerModel?> GetCustomerByIdAsync(string id)
    {
        var response = await _httpClient.GetFromJsonAsync<CustomerModel>($"customers/{id}");
        return response ?? null;
    }


    // Fetch orders for a specific customer
    public async Task<List<OrderModel>> GetOrdersByCustomerAsync(string customerId)
    {
        var response = await _httpClient.GetFromJsonAsync<List<OrderModel>>($"orders?customerId={customerId}");
        return response ?? new List<OrderModel>();
    }

    // Add a new order for a customer
    public async Task<bool> AddOrderAsync(OrderModel order)
    {
        var response = await _httpClient.PostAsJsonAsync("orders", order);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCustomerAsync(string customerId)
    {
        var response = await _httpClient.DeleteAsync($"customers/{customerId}"); 
        return response.IsSuccessStatusCode;
    }
}