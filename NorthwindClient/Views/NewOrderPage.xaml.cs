using NorthwindClient.Models;
using NorthwindClient.ViewModels;

namespace NorthwindClient.Views;

public partial class NewOrderPage : ContentPage, IQueryAttributable
{
    private NewOrderViewModel _viewModel;

    public NewOrderPage(NewOrderViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object>? query)
    {
        if (query?.TryGetValue("customer", out var value) is true && value is CustomerModel customer)
        {
            _viewModel.InitOrder(customer);
        }
        else
        {
            DisplayAlert("Error", "No customer data available.", "OK");
        }
    }
}