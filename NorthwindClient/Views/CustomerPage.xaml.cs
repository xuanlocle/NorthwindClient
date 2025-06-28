using NorthwindClient.ViewModels;

namespace NorthwindClient.Views;

public partial class CustomerPage : ContentPage
{

    public CustomerPage(CustomerViewModel viewmodel)
    {
        InitializeComponent();
        BindingContext = viewmodel;
    }

}