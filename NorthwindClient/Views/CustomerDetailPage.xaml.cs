using NorthwindClient.ViewModels;

namespace NorthwindClient.Views;

public partial class CustomerDetailPage : ContentPage, IQueryAttributable
{
    private readonly CustomerDetailsViewModel _viewModel;
    
    public CustomerDetailPage(CustomerDetailsViewModel viewModel)
    {
        InitializeComponent(); 
        BindingContext = _viewModel = viewModel;  
    }

    public void ApplyQueryAttributes(IDictionary<string, object>? query)
    {
        if (query?.TryGetValue("customerId", out var value) is true)
        {
            var customerId = value as string;
            if (!string.IsNullOrEmpty(customerId))
            {
                _viewModel.LoadCustomerDetails(customerId);
            }
                
        }
        else
        {
            DisplayAlert("Error", "No customer data available.", "OK");
        }
    }
}