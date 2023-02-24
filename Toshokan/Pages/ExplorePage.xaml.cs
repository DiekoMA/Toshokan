using System.Windows.Controls;
using AniListNet;
using AniListNet.Objects;
using AniListNet.Parameters;

namespace Toshokan.Pages;

public partial class ExplorePage : Page
{
    private AniClient _client;
    public ExplorePage()
    {
        InitializeComponent();
        _client = new AniClient();
    }

    private async void ExploreSearchBar_OnSearchStarted(object? sender, FunctionEventArgs<string> e)
    {
        switch (QueryTypeComboBox.SelectedIndex)
        {
            case 1:
                var animes = await _client.SearchMediaAsync(new SearchMediaFilter
                {
                    Query = ExploreSearchBar.Text,
                    Type = MediaType.Anime
                });
                if (QueryResultBox?.Items == null)
                {
                    foreach (var anime in animes.Data)
                    {
                        QueryResultBox.Items.Add(anime.Title.EnglishTitle + "\n" + anime.Title.NativeTitle);
                    }    
                }
                else
                {
                    QueryResultBox.Items.Clear();
                    foreach (var anime in animes.Data)
                    {
                        QueryResultBox.Items.Add(anime.Title.EnglishTitle + "\n" + anime.Title.NativeTitle);
                    }
                }
                
                break;
            
            case 2:
                var mangas = await _client.SearchMediaAsync(new SearchMediaFilter
                {
                    Query = ExploreSearchBar.Text,
                    Type = MediaType.Manga
                });
                if (QueryResultBox?.Items == null)
                {
                    foreach (var manga in mangas.Data)
                    {
                        QueryResultBox.Items.Add(manga.Title.EnglishTitle);
                    }

                    ListPagination.MaxPageCount = mangas.LastPageIndex;
                }
                else
                {
                    QueryResultBox.Items.Clear();
                    foreach (var manga in mangas.Data)
                    {
                        QueryResultBox.Items.Add(manga.Title.EnglishTitle + "\n" + manga.Title.NativeTitle);
                    }
                    ListPagination.MaxPageCount = mangas.LastPageIndex;
                }
                break;
            
            case 3:
                var users = await _client.SearchUserAsync(ExploreSearchBar.Text);
                if (QueryResultBox?.Items == null)
                {
                    foreach (var user in users.Data)
                    {
                        QueryResultBox.Items.Add(user.Name);
                    }    
                }
                else
                {
                    QueryResultBox.Items.Clear();
                    foreach (var user in users.Data)
                    {
                        QueryResultBox.Items.Add(user.Name);
                    }
                }
                break;
            
            case 4:
                var staffs = await _client.SearchStaffAsync(ExploreSearchBar.Text);
                if (QueryResultBox?.Items == null)
                {
                    foreach (var staff in staffs.Data)
                    {
                        QueryResultBox.Items.Add(staff.Name.FirstName);
                    }    
                }
                else
                {
                    QueryResultBox.Items.Clear();
                    foreach (var staff in staffs.Data)
                    {
                        QueryResultBox.Items.Add(staff.Name.FirstName);
                    }
                }
                break;
            
            case 5:
                var characters = await _client.SearchCharacterAsync(ExploreSearchBar.Text);
                if (QueryResultBox?.Items == null)
                {
                    foreach (var character in characters.Data)
                    {
                        QueryResultBox.Items.Add(character.Name.FirstName);
                    }    
                }
                else
                {
                    QueryResultBox.Items.Clear();
                    foreach (var character in characters.Data)
                    {
                        QueryResultBox.Items.Add(character.Name.FirstName);
                    }
                }
                break;
        }
    }
}