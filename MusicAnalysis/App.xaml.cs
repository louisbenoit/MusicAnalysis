using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicAnalysis
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            testOfSpotify();

            MainPage = new NavigationPage(new LoginPage());
        }

        private void testOfSpotify()
        {
            var credentials = "a3f9f652c64149a49a869f8391711492:19933680108d4bb6885d14f2d8b0dcd8";
            var credentialsByteArray = System.Text.ASCIIEncoding.ASCII.GetBytes(credentials);
            var base64Credentials = System.Convert.ToBase64String(credentialsByteArray);

            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string> { { "grant_type", "client_credentials" } };
            var content = new FormUrlEncodedContent(values);

            client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Basic", base64Credentials);

            var response = client.PostAsync("https://accounts.spotify.com/api/token", content).Result;

            var responseString = response.Content.ReadAsStringAsync().Result;
            
            var resposeDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);

            Console.WriteLine(resposeDictionary["access_token"]);

            HttpClient newClient = new HttpClient();

            newClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resposeDictionary["access_token"]);
            var newResponse = newClient.GetAsync("https://api.spotify.com/v1/tracks/2TpxZ7JUBn3uw46aR7qd6V").Result;
            var newResponseString = newResponse.Content.ReadAsStringAsync().Result;
            var newResponseDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(newResponseString);

            // Client ID a3f9f652c64149a49a869f8391711492
            // Client Secret 19933680108d4bb6885d14f2d8b0dcd8
            // YTNmOWY2NTJjNjQxNDlhNDlhODY5ZjgzOTE3MTE0OTI6MTk5MzM2ODAxMDhkNGJiNjg4NWQxNGYyZDhiMGRjZDg
            SpotifyWebAPI api = new SpotifyWebAPI
            {
                AccessToken = "XX?X?X",
                TokenType = "Bearer"
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
