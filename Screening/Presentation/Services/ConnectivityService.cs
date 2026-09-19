using Screening.Presentation.Services.Abstraction;

namespace Screening.Presentation.Services;

public sealed class ConnectivityService : IConnectivityService, IDisposable
{
    public event EventHandler<bool>? ConnectivityChanged;

    public ConnectivityService()
    {
        NetworkInformation.NetworkStatusChanged += OnNetworkStatusChanged;
    }

    public bool IsConnected
    {
        get
        {
            var profile = NetworkInformation.GetInternetConnectionProfile();
            return profile?.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
        }
    }

    private void OnNetworkStatusChanged(object sender)
    {
        ConnectivityChanged?.Invoke(this, IsConnected);
    }

    public void Dispose()
    {
        NetworkInformation.NetworkStatusChanged -= OnNetworkStatusChanged;
    }
}
