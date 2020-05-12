using System;
using MusicAnalysis.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicAnalysis
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        SearchResult viewModel;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new SearchResult();
        }
        async void SearchClicked(object sender, EventArgs e)
        {
            SearchBar sb = FindByName("AlbumSearch") as SearchBar;
            await viewModel.SearchAlbums(sb.Text);
        }
        async void ImageButtonClicked(object sender, EventArgs e)
        {
            ImageButton ib = FindByName("SelectedAlbum") as ImageButton;
            await Navigation.PushAsync(new AlbumPage(ib.AutomationId));
            
        }
        //async Task<object> SearchClicked(object sender, EventArgs e)
        //{
        //    string text = ((Entry)sender).Text;
        //    //return api.GetSeveralTracksAsync((List<string> ids, string market = "");

        //}
    }
}
