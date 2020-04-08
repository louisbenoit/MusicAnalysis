using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MusicAnalysis
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        async void UsernameCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
        }
        async void PasswordCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
        }
        private async void LoginClicked(object sender, EventArgs e)
        {
            GET https://accounts.spotify.com/authorize?client_id=5fe01282e44241328a84e7c5cc169165&response_type=code&redirect_uri=https%3A%2F%2Fexample.com%2Fcallback&scope=user-read-private%20user-read-email&state=34fFs29kd09
            //await Navigation.PushAsync(new MainPage());
        }
    }
}
