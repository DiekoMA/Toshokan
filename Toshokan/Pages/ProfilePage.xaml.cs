using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Toshokan.Pages;

public partial class ProfilePage : Page
{
    public ProfilePage()
    {
        InitializeComponent();
        //ProfileImage.Source = new BitmapImage(AniilistHandler.Instance.GetUserInfo().Result.Avatar.MediumImageUrl);
        //UsernameText.Text = AniilistHandler.Instance._aniClient.GetAuthenticatedUserAsync().Result.Name;
    }
}