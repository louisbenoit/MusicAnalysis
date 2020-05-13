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
            viewModel.FullAlbumGetAsync().Wait();

        }

    }
}
