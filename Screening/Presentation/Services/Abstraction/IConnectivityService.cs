namespace Screening.Presentation.Services.Abstraction;

public interface IConnectivityService
{
    bool IsConnected { get; }
    event EventHandler<bool>? ConnectivityChanged;
}
