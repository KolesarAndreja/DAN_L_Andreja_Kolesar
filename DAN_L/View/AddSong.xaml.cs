using DAN_L.Service;
using DAN_L.ViewModel;
using System.Windows;
namespace DAN_L.View
{
    /// <summary>
    /// Interaction logic for AddSong.xaml
    /// </summary>
    public partial class AddSong : Window
    {
        public AddSong()
        {
            InitializeComponent();
        }
        public AddSong(tblUser user)
        {
            InitializeComponent();
            this.DataContext = new AddSongViewModel(this, user);
        }
    }
}
