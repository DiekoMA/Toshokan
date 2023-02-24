using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using Toshokan.Pages;

namespace Toshokan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private LibraryPage _libraryPage;
        private ExplorePage _explorePage;
        private SchedulePage _schedulePage;
        private ProfilePage _profilePage;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LibrarySideMenuItem_OnSelected(object sender, RoutedEventArgs e)
        {
            _libraryPage = new LibraryPage();
            ContentFrame.Content = _libraryPage;
        }

        private void ExploreSideMenuItem_OnSelected(object sender, RoutedEventArgs e)
        {
            _explorePage = new ExplorePage();
            ContentFrame.Content = _explorePage;
        }

        private void SettingsSideMenuItem_OnSelected(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        private void ScheduleSideMenuItem_OnSelected(object sender, RoutedEventArgs e)
        {
            _schedulePage = new SchedulePage();
            ContentFrame.Content = _schedulePage;
        }
        
        private void AccountSideMenuItem_OnSelected(object sender, RoutedEventArgs e)
        {
            _profilePage = new ProfilePage();
            ContentFrame.Content = _schedulePage;
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _profilePage = new ProfilePage();
                ContentFrame.Content = _profilePage;
            }
            catch (Exception exception)
            {
                Growl.Error(exception.Message);
            }
        }

        private void MainWindow_OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                var file = files[0];
                MessageBox.Show(file);
            }
        }

        
    }
}