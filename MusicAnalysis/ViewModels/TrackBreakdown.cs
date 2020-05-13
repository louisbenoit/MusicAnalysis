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
    public class TrackExtended
    {
        public string Url { get; set; }
        public string ID { get; set; }
        public string Artist { get; set; }
        public string TrackName { get; set; }
        public float Acousticness { get; set; }
        public float Danceability { get; set; }
        public float Energy { get; set; }
        public float Instrumentalness { get; set; }
        public float Loudness { get; set; }
        public float Tempo { get; set; }
        public int Key { get; set; }
    }
    public class TrackBreakdown
    {
        public ObservableCollection<TrackExtended> Tracks { get; set; }
        private string BearerToken;
        private string ID;

        public TrackBreakdown(String ID)
        {
            this.ID = ID;
            //GetBearerToken().Wait();
            BearerToken = Application.Current.Properties["BearerToken"].ToString();
            Tracks = new ObservableCollection<TrackExtended>();

        }
        public async Task<bool> TrackBreakdownGet()
        {
            //To get all of the info for the track you need to make a few calls
            //FullTrack: Artist (string)
            //           Album (string)
            //           Name (string)
            //AudioFeatures: Acousticness (float)
            //               Danceability (float)
            //               Energy (float)
            //               Instrumentalness (float)
            //               Key (int)
            //               Loudness (float)
            //               Tempo (float)
            //FullAlbum : Url (string) (for image)
            SpotifyWebAPI api = new SpotifyWebAPI
            {
                AccessToken = BearerToken,
                TokenType = "Bearer"
            };
            //get the FullTrack
            var fullTrack = await api.GetTrackAsync(ID);
            //get the AudioFeatures
            var trackAudioFeatures = await api.GetAudioFeaturesAsync(ID);
            // get the FullAlbum
            var fullAlbum = await api.GetAlbumAsync(ID);
            TrackExtended trackExtended = new TrackExtended();

            //trackExtended.Url = fullAlbum.Images[0].Url;
            trackExtended.Artist = fullTrack.Artists[0].Name;
            trackExtended.TrackName = fullTrack.Name;
            trackExtended.Acousticness = trackAudioFeatures.Acousticness;
            trackExtended.Danceability = trackAudioFeatures.Danceability;
            trackExtended.Energy = trackAudioFeatures.Energy;
            trackExtended.Instrumentalness = trackAudioFeatures.Instrumentalness;
            trackExtended.Key = trackAudioFeatures.Key;
            trackExtended.Loudness = trackAudioFeatures.Loudness;
            trackExtended.Tempo = trackAudioFeatures.Tempo;

            Tracks.Add(trackExtended);

            return true;
        }


    }
}
