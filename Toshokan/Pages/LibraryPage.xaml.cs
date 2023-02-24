using System.Threading;
using System.Windows.Controls;
using AniListNet.Objects;
using HandyControl.Controls;
using Toshokan.Utils;
using TabItem = System.Windows.Controls.TabItem;

namespace Toshokan.Pages;

public partial class LibraryPage : Page
{
    public LibraryPage()
    {
        InitializeComponent();
        if(AniilistHandler.Instance._aniClient.IsAuthenticated == true)
        {
            Growl.Success("Authenticated");
        }else{Growl.Error("Not Authenticated");}
        // GetLists();
    }

    private async void GetLists()
    {
        try
        {
            var user = await AniilistHandler.Instance._aniClient.GetAuthenticatedUserAsync();

            var userLists = await AniilistHandler.Instance._aniClient.GetUserEntryCollectionAsync(user.Id, MediaType.Anime);
            Grid tabControlItemGrid = new Grid();
            ListBox listEntryBox = new ListBox();
            
            foreach (var list in userLists.Lists)
            {
                TabItem listTabItem = new TabItem();
                listTabItem.Header = list.Name;
                if (string.IsNullOrWhiteSpace(list.Name))
                {
                    list.Name.Replace(" ", "");
                    listTabItem.Name = string.Concat(list.Name ,"TabItem");
                }
                ListTabControl.Items.Add(listTabItem);
                foreach (var entry in list.Entries)
                {
                    listEntryBox.Items.Add(entry.Media.Title.EnglishTitle);
                }
                tabControlItemGrid.Children.Add(listEntryBox);
                listTabItem.Content = tabControlItemGrid;
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
        
        
    }

    private async void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        
    }
}