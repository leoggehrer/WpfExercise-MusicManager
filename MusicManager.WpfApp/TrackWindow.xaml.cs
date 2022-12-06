using System.Windows;

namespace MusicManager.WpfApp
{
    /// <summary>
    /// Interaction logic for AlbumWindow.xaml
    /// </summary>
    public partial class TrackWindow : Window
    {
        public TrackWindow()
        {
            InitializeComponent();
            ViewModel.CloseWindow = () => Close();
        }
    }
}
