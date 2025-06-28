using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using NorthwindClient.Infrastructure;
using NorthwindClient.Models;
using NorthwindClient.Services;
using NorthwindClient.Views;

namespace NorthwindClient.ViewModels;

public partial class NewOrderViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IApiService _service;
    [ObservableProperty] private OrderModel order = new(); // this triggers updates

    [ObservableProperty] private CustomerModel? _customer;

    [ObservableProperty] private DateTime? _orderDate;
    [ObservableProperty] private TimeSpan? _orderTime;

    [ObservableProperty] private DateTime? _requiredDate;
    [ObservableProperty] private TimeSpan? _requiredTime;

    [ObservableProperty] private DateTime? _shippedDate;
    [ObservableProperty] private TimeSpan? _shippedTime;

    [ObservableProperty] private decimal? freight;
    [ObservableProperty] private string? shipName;
    [ObservableProperty] private string? shipAddress;
    [ObservableProperty] private string? shipCity;
    [ObservableProperty] private string? shipRegion;
    [ObservableProperty] private string? shipPostalCode;
    [ObservableProperty] private string? shipCountry;

    public DateTime? OrderDateTime
    {
        get => OrderDate.HasValue && OrderTime.HasValue
            ? OrderDate.Value.Date + OrderTime.Value
            : null;
    }

    public DateTime? RequiredDateTime
    {
        get => RequiredDate.HasValue && RequiredTime.HasValue
            ? RequiredDate.Value.Date + RequiredTime.Value
            : null;
    }

    public DateTime? ShippedDateTime
    {
        get => ShippedDate.HasValue && ShippedTime.HasValue
            ? ShippedDate.Value.Date + ShippedTime.Value
            : null;
    }

    public NewOrderViewModel(INavigationService navigationService, IApiService service)
    {
        _navigationService = navigationService;
        _service = service;

        OrderDate = DateTime.Now;
        RequiredDate = DateTime.Now.AddDays(1);
        ShippedDate = DateTime.Now.AddDays(2);
        OrderTime = TimeSpan.FromHours(12); // Default 12:00 PM
        RequiredTime = TimeSpan.FromHours(10); // Default 10:00 AM
        ShippedTime = TimeSpan.FromHours(15); // Default 3:00 PM
    }

    public void InitOrder(CustomerModel customer)
    {
        _customer = customer;
        OrderDate = DateTime.Now;
        RequiredDate = DateTime.Now.AddDays(1);
        ShippedDate = DateTime.Now.AddDays(2);
        OrderTime = TimeSpan.FromHours(12); // Default 12:00 PM
        RequiredTime = TimeSpan.FromHours(10); // Default 10:00 AM
        ShippedTime = TimeSpan.FromHours(15); // Default 3:00 PM
    }


    [RelayCommand]
    private async void SubmitOrder()
    {
        try
        {
            if (ValidateOrder())
            {
                var order = new OrderModel()
                {
                    CustomerId = _customer.CustomerId,

                    OrderDate = OrderDateTime,
                    RequiredDate = RequiredDateTime,
                    ShippedDate = ShippedDateTime,
                    Freight = Freight,
                    ShipName = ShipName,
                    ShipAddress = ShipAddress,
                    ShipCity = ShipCity,
                    ShipRegion = ShipRegion,
                    ShipPostalCode = ShipPostalCode,
                    ShipCountry = ShipCountry
                };

                // Submit the order to the API
                var result = await _service.AddOrderAsync(order);

                if (result)
                {
                    await Shell.Current.DisplayAlert("Success", "Order submitted successfully", "OK");
                    await _navigationService.GoBackAsync();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Failed to submit the order. Please try again.", "OK");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Please fill in all fields correctly.", "OK");
            }
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Error", e.Message, "OK");
        }
    }

    // Validate the order before submission
    private bool ValidateOrder()
    {
        // Validate required fields (e.g., OrderDate, CustomerId, etc.)
        if (string.IsNullOrEmpty(_customer.CustomerId) || !OrderDateTime.HasValue)
        {
            return false;
        }

        // Add further validation if needed
        return true;
    }

    // Simulate scanning the barcode
    [RelayCommand]
    private async Task Scan(string target)
    {
        var popup = new ScanBarcodePopup();
        popup.OnScanned = (scannedValue) =>
        {
            if (!scannedValue.All(char.IsDigit))
            {
                Shell.Current.DisplayAlert("Error", "Failed to submit the order. Please try again.", "OK");
                return;
            }

            switch (target)
            {
                case "freight":
                    Freight = int.Parse(scannedValue);
                    break;
                case "shipvia":
                    Order.ShipVia = int.Parse(scannedValue);
                    break;
            }
        };

        await MopupService.Instance.PushAsync(popup);
    }

    [RelayCommand]
    private void ClearForm()
    {
        // Reset order object
        Order = new OrderModel()
        {
            CustomerId = _customer.CustomerId
        };


        // Reset fields
        Freight = null;
        ShipName = null;
        ShipAddress = null;
        ShipCity = null;
        ShipRegion = null;
        ShipPostalCode = null;
        ShipCountry = null;

        // Reset date/time fields
        OrderDate = DateTime.Now;
        RequiredDate = DateTime.Now.AddDays(1);
        ShippedDate = DateTime.Now.AddDays(2);

        OrderTime = TimeSpan.FromHours(12); // 12:00 PM
        RequiredTime = TimeSpan.FromHours(10); // 10:00 AM
        ShippedTime = TimeSpan.FromHours(15); // 3:00 PM
    }
}