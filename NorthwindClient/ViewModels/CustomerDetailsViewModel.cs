using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NorthwindClient.Common;
using NorthwindClient.Infrastructure;
using NorthwindClient.Models;
using NorthwindClient.Services;

namespace NorthwindClient.ViewModels;

public partial class CustomerDetailsViewModel(INavigationService navigationService, IApiService apiService)
    : ObservableObject
{
    [ObservableProperty] private CustomerModel? _customer;

    [ObservableProperty] private bool _isLoading;

    
    
    // Load customer details based on some customer ID (assuming a static ID for now)
    public async void LoadCustomerDetails(string id)
    {
        IsLoading = true;
        try
        {
            // Replace with actual API call and customer ID
            var result = await apiService.GetCustomerByIdAsync(id);
            if (result == null)
            {
                await navigationService.GoBackAsync();
                return;
            }

            Customer = result;
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Error", "Fail to get details. Please check network and try again.", "OK");
            await navigationService.GoBackAsync();
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task GoToNewOrder()
    {
        try
        {
            await navigationService.NavigateToAsync(Routes.NewOrderPage,
                new Dictionary<string, object> { { "customer", Customer } });
        }
        catch (Exception e)
        {
            await Toast.Make($"Error: {e.Message}").Show();
        }
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await navigationService.GoBackAsync();
    }
}