using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Input;
using Serilog;

namespace Toshokan
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            #region Logging/Crash Handling
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.txt")
                .CreateLogger();
            #endregion

            #region File Checks
            byte[] key = { 0x02, 0x03, 0x01, 0x03, 0x03, 0x07, 0x07, 0x08, 0x09, 0x09, 0x11, 0x11, 0x16, 0x17, 0x19, 0x16 };
            var tokenFile = Path.Combine(Directory.GetCurrentDirectory(), "token.secret");
            if (File.Exists(tokenFile))
            {
                try
                {
                    using FileStream tokStream = new FileStream(tokenFile, FileMode.Open);

                    using Aes aes = Aes.Create();

                    byte[] iv = new byte[aes.IV.Length];
                    tokStream.Read(iv, 0, iv.Length);

                    using CryptoStream cryptStream = new CryptoStream(
                        tokStream,
                        aes.CreateDecryptor(key, iv),
                        CryptoStreamMode.Read);

                    using StreamReader sReader = new StreamReader(cryptStream);
                
                    var accessToken = sReader.ReadToEnd().Replace(" ", "");
                    //MessageBox.Show(accessToken);
                    AniilistHandler.Instance._aniClient.TryAuthenticateAsync(accessToken);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
            else
            {
                
            }
            
            var configFile = Path.Combine(Directory.GetCurrentDirectory(), "appconfig.cfg");
            if (!File.Exists(configFile))
            {
                try
                {
                    File.Create(configFile);
                    AppSettingsHelper.Instance.SaveBoolItem("Preferences", "firststartup", false);
                    StartupUri = new Uri("Dialogs/FirstStartDialog.xaml", UriKind.Relative);
                }
                catch (Exception exception)
                {
                    Growl.Error(exception.Message + "Has been logged");
                    Log.Error(exception.Message);
                }
            }
            else
            {
                StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
            }

            #endregion
            
            #region JumpList Code
            var jtLibrary = new JumpTask
            {
                Title = "Library",
                Description = "Library section of Toshokan",
                IconResourceIndex = 2,
                IconResourcePath = Path.Combine(Directory.GetCurrentDirectory(), "Icons.dll"),
                WorkingDirectory = Directory.GetCurrentDirectory(),
                ApplicationPath = Process.GetCurrentProcess().MainModule?.FileName,
                CustomCategory = "Navigation"
            };

            var jtExplore = new JumpTask
            {
                Title = "Explore",
                Description = "Explore section of Toshokan",
                IconResourceIndex = 1,
                IconResourcePath = Path.Combine(Directory.GetCurrentDirectory(), "Icons.dll"),
                WorkingDirectory = Directory.GetCurrentDirectory(),
                ApplicationPath = Process.GetCurrentProcess().MainModule?.FileName,
                CustomCategory = "Navigation"
            };
            
            var jtCalendar = new JumpTask
            {
                Title = "Calendar",
                Description = "Calendar section of Toshokan",
                IconResourceIndex = 0,
                IconResourcePath = Path.Combine(Directory.GetCurrentDirectory(), "Icons.dll"),
                WorkingDirectory = Directory.GetCurrentDirectory(),
                ApplicationPath = Process.GetCurrentProcess().MainModule?.FileName,
                CustomCategory = "Navigation",
            };
            
            var jtTrending = new JumpTask
            {
                Title = "Trending",
                Description = "Trending section of Toshokan",
                IconResourceIndex = 3,
                IconResourcePath = Path.Combine(Directory.GetCurrentDirectory(), "Icons.dll"),
                ApplicationPath = Process.GetCurrentProcess().MainModule?.FileName,
                CustomCategory = "Navigation",
                Arguments = "LibraryPage"
            };
            
            var currentJumpList = JumpList.GetJumpList(App.Current);
            currentJumpList.JumpItems.Add(jtLibrary);
            currentJumpList.JumpItems.Add(jtExplore);
            currentJumpList.JumpItems.Add(jtCalendar);
            currentJumpList.JumpItems.Add(jtTrending);
            currentJumpList.Apply();
            #endregion
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.CloseAndFlush();
        }
    }
}