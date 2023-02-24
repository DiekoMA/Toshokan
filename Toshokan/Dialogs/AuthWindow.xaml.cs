using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.Web.WebView2.Core;

namespace Toshokan.Dialogs;

public partial class AuthWindow
{
    public AuthWindow()
    {
        InitializeComponent();
        //AniilistHandler.Instance.Authenticate();
    }


    private void AuthView_OnNavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
    {
        var tokenRegex = new Regex("(?<=access_token=)(.*)(?=&token_type)");
        if (!tokenRegex.IsMatch(e.Uri))
            return;
        var accessToken = tokenRegex.Match(e.Uri).Value;
    }

    private void AuthView_OnNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        if (authView.Source.AbsoluteUri == "https://anilist.co/api/v2/oauth/authorize?client_id=10344&response_type=token")
        {
            
        }
        else
        {
            var tokenRegex = new Regex("(?<=access_token=)(.*)(?=&token_type)");
            if (!tokenRegex.IsMatch(authView.Source.AbsoluteUri))
                return;
            var accessToken = tokenRegex.Match(authView.Source.AbsoluteUri).Value;
            
            byte[] key = { 0x02, 0x03, 0x01, 0x03, 0x03, 0x07, 0x07, 0x08, 0x09, 0x09, 0x11, 0x11, 0x16, 0x17, 0x19, 0x16 };

            var file = Path.Combine(Directory.GetCurrentDirectory(), "token.secret");
            using FileStream tokStream = new FileStream(file ,FileMode.OpenOrCreate);
            
            using Aes aes = Aes.Create();
            aes.Key = key;

            byte[] iv = aes.IV;
            
            tokStream.Write(iv, 0, iv.Length);

            using CryptoStream cryptStream = new CryptoStream(
                tokStream,
                aes.CreateEncryptor(),
                CryptoStreamMode.Write);

            using StreamWriter sWriter = new StreamWriter(cryptStream);
            sWriter.WriteLine(accessToken);
        }
    }
}