using NorthwindClient.Infrastructure;

namespace NorthwindClient;

public partial class AppShell : Shell
{
    public AppShell(IRouteRegistrar routeRegistrar)
    {
        InitializeComponent();
        routeRegistrar.RegisterRoutes();
    }

}