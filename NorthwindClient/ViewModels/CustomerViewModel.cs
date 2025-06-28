using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using NorthwindClient.Common;
using NorthwindClient.Infrastructure;
using NorthwindClient.Models;
using NorthwindClient.Services;

namespace NorthwindClient.ViewModels;

public partial class CustomerViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IApiService _service;

    [ObservableProperty] private ObservableCollection<CustomerModel>? _customers;

    [ObservableProperty] private CustomerModel? selectedCustomer;

    // State properties for managing UI states
    [ObservableProperty] private bool isLoading = true; // Set true initially for loading state
    [ObservableProperty] private bool isError = false; // Default to no error
    [ObservableProperty] private string errorMessage = string.Empty; // To display error messages
    [ObservableProperty] private bool isCustomerSizeGreaterThanZero = true;


    public CustomerViewModel(INavigationService navigationService, IApiService service)
    {
        _navigationService = navigationService;
        _service = service;
        LoadCustomerCommand.Execute(null);
    }


    [RelayCommand]
    private async Task LoadCustomerAsync()
    {
        IsLoading = true;
        IsError = false;
        try
        {
            var result = await _service.GetCustomersAsync();
            Customers = new ObservableCollection<CustomerModel>(result);
            IsCustomerSizeGreaterThanZero = Customers?.Count > 0;
        }
        catch (Exception ex)
        {
            IsError = true;
            ErrorMessage = ex.Message;
            IsCustomerSizeGreaterThanZero = false;
            await ShowErrorMessage("Failed to load customers: " + ex.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task ShowErrorMessage(string message)
    {
        // Display an error toast
        var errorToast = Toast.Make(message, ToastDuration.Short);
        await errorToast.Show();
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await Task.Delay(1000);
        LoadCustomerCommand.Execute(null);
    }

    [RelayCommand]
    private async Task DeleteCustomer(string customerId)
    {
        var result = await _service.DeleteCustomerAsync(customerId);

        if (result)
        {
            var successToast = Toast.Make("Customer deleted successfully!", ToastDuration.Short);
            await successToast.Show();
            LoadCustomerCommand.Execute(null);
        }
        else
        {
            var errorToast = Toast.Make("Error deleting customer. Please try again.", ToastDuration.Short);
            await errorToast.Show();
            // Handle failure
        }
    }

    [RelayCommand]
    private async Task CustomerSelected(string customerId)
    {
        if (customerId == null)
            return;

        await _navigationService.NavigateToAsync(
            Routes.CustomerDetailPage,
            new Dictionary<string, object> { { "customerId", customerId } });
    }
}