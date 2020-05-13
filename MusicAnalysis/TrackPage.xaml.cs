using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicAnalysis.ViewModels;
using Xamarin.Forms;

namespace MusicAnalysis
{
    public partial class TrackPage : ContentPage
    {
        TrackBreakdown viewModel;
        public TrackPage(string ID)
        {
            InitializeComponent();
            BindingContext = viewModel = new TrackBreakdown(ID);
            Task.Run(async () => { await viewModel.TrackBreakdownGet(); });
        }
    }
}
