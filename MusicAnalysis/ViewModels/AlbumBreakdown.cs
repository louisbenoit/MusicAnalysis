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
using Xamarin.Forms;

namespace MusicAnalysis.ViewModels
{
    public class MyTrack
    {
        public string ID { get; set; }
        public string Artist { get; set; }
        public string TrackName { get; set; }

    }

    public class AlbumBreakdown
    {
        public ObservableCollection<MyTrack> Tracks { get; set; }
        private string BearerToken;
        private string ID;

        public AlbumBreakdown(String ID)
        {
            this.ID = ID;
            //GetBearerToken().Wait();
            BearerToken = Application.Current.Properties["BearerToken"].ToString();
            Tracks = new ObservableCollection<MyTrack>();
        }

        //Get the bearer token for Spotify
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

        public async Task<bool> FullAlbumGetAsync()
        {
            SpotifyWebAPI api = new SpotifyWebAPI
            {
                AccessToken = BearerToken,
                TokenType = "Bearer"
            };

            var album = await api.GetAlbumAsync(ID);

            Tracks.Clear();

            if (album.TotalTracks > 0)
            {
                for (int index = 0; index < album.TotalTracks; index++) //(Changed from foreach)var track in album.Tracks.Items)
                {
                    var addedTrack = new MyTrack { TrackName = album.Tracks.Items[index].Name, ID = album.Tracks.Items[index].Id };
                    Tracks.Add(addedTrack);
                }
            }

            return true;
        }        
    }
}
