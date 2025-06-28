namespace NorthwindClient.Infrastructure;

public interface INavigationService
{
    Task NavigateToAsync(string route, IDictionary<string, object>? parameters = null);
    Task GoBackAsync(IDictionary<string, object>? parameters = null);
}
