using System.Windows;

namespace Toshokan.Dialogs;

public partial class SettingsWindow 
{
    public SettingsWindow()
    {
        InitializeComponent();
    }

    private void StartAuthFlow(object sender, RoutedEventArgs e)
    {
        AuthWindow authWindow = new AuthWindow();
        authWindow.ShowDialog();
    }
}