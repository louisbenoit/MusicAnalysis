using SpotifyAPI.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicAnalysis
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        SpotifyWebAPI api = new SpotifyWebAPI
        {
            AccessToken = "XX?X?X",
            TokenType = "Bearer"
        };
        public MainPage()
        {
            InitializeComponent();

        }
        //async Task<object> SearchClicked(object sender, EventArgs e)
        //{
        //    string text = ((Entry)sender).Text;
        //    //return api.GetSeveralTracksAsync((List<string> ids, string market = "");

        //}
    }
}
