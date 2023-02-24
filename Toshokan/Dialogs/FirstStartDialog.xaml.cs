using System.Windows;

namespace Toshokan.Dialogs;

public partial class FirstStartDialog
{
    public FirstStartDialog()
    {
        InitializeComponent();
    }

    private void ButtonBack_OnClick(object sender, RoutedEventArgs e)
    {
        StartupStepBar.Prev();
    }

    private void ButtonNext_OnClick(object sender, RoutedEventArgs e)
    {
        StartupStepBar.Next();
    }
}