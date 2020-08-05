using DAN_L.Service;
using DAN_L.ViewModel;
using System.Windows;

namespace DAN_L.View
{
    /// <summary>
    /// Interaction logic for AudioPlayer.xaml
    /// </summary>
    public partial class AudioPlayer : Window
    {
        public AudioPlayer()
        {
            InitializeComponent();
        }

        public AudioPlayer(tblUser user)
        {
            InitializeComponent();
            this.DataContext = new AudioPlayerViewModel(this, user);
        }
    }
}
