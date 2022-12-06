using System.Windows;

namespace MusicManager.WpfApp
{
    /// <summary>
    /// Interaction logic for AlbumWindow.xaml
    /// </summary>
    public partial class AlbumWindow : Window
    {
        public AlbumWindow()
        {
            InitializeComponent();
            ViewModel.CloseWindow = () => Close();
        }
    }
}
