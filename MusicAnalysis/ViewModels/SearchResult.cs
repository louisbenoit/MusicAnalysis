using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;

namespace MusicAnalysis.ViewModels
{
    public class SearchResult 
    {
        public ObservableCollection<Image> Albums { get; set; }

        private string BearerToken;

        public SearchResult()
        {
            Albums = new ObservableCollection<Image>();
            GetBearerToken().Wait();
        }

        private async Task<bool> GetBearerToken()
        {
            var credentials = "a3f9f652c64149a49a869f8391711492:19933680108d4bb6885d14f2d8b0dcd8";
            var credentialsByteArray = System.Text.ASCIIEncoding.ASCII.GetBytes(credentials);
            var base64Credentials = System.Convert.ToBase64String(credentialsByteArray);

            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string> { { "grant_type", "client_credentials" } };
            var content = new FormUrlEncodedContent(values);

            client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Basic", base64Credentials);

            var response = await client.PostAsync("https://accounts.spotify.com/api/token", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var responseDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);

            BearerToken = responseDictionary["access_token"];

            return true;
        }

        public async Task<bool> SearchAlbums(string searchString)
        {
            SpotifyWebAPI api = new SpotifyWebAPI
            {
                AccessToken = BearerToken,
                TokenType = "Bearer"
            };

            var searchResult = await api.SearchItemsAsync(searchString, SpotifyAPI.Web.Enums.SearchType.Album);

            Albums.Clear();

            if (null != searchResult.Albums)
            {
                if (searchResult.Albums.Items.Count > 0)
                {
                    foreach (var album in searchResult.Albums.Items)
                    {
                        if (album.Images.Count > 0)
                        {
                            if (album.Images[0] is Image)
                            {
                                Console.WriteLine(album.Images[0].Url);
                                Albums.Add(album.Images[0] as Image);
                            }
                        }
                    }
                }
            }

            return true;
        }
    }
}
