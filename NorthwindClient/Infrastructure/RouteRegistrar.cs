using NorthwindClient.Common;
using NorthwindClient.Views;

namespace NorthwindClient.Infrastructure;

public class RouteRegistrar : IRouteRegistrar
{
    public void RegisterRoutes()
    {
        Routing.RegisterRoute(Routes.CustomerPage, typeof(CustomerPage));
        Routing.RegisterRoute(Routes.CustomerDetailPage, typeof(CustomerDetailPage));
        Routing.RegisterRoute(Routes.NewOrderPage, typeof(NewOrderPage));
    }
}