using Mopups.Pages;
using Mopups.Services;
using ZXing.Net.Maui;

namespace NorthwindClient.Views;

public partial class ScanBarcodePopup : PopupPage
{
    public Action<string>? OnScanned { get; set; }

    public ScanBarcodePopup()
    {
        InitializeComponent();
    }

    private void OnBarcodeDetected(object sender, BarcodeDetectionEventArgs e)
    {
        var value = e.Results.FirstOrDefault()?.Value;
        if (!string.IsNullOrEmpty(value))
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                OnScanned?.Invoke(value);
                await MopupService.Instance.PopAsync();
            });
        }
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }
}