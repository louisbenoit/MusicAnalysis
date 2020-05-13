using System;
using System.Threading.Tasks;
using MusicAnalysis.ViewModels;
using Xamarin.Forms;

namespace MusicAnalysis
{
    public partial class AlbumPage : ContentPage
    {
        AlbumBreakdown viewModel;
        public AlbumPage(string ID)
        {
            InitializeComponent();
            BindingContext = viewModel = new AlbumBreakdown(ID);
            Task.Run(async () => { await viewModel.FullAlbumGetAsync(); });
        }
        async void TrackClicked(object sender, EventArgs e)
        {
            Button tb = sender as Button;
            await Navigation.PushAsync(new TrackPage(tb.AutomationId));

        }
    }
}
