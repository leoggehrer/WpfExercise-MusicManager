using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using Logic = Repository.Logic;

namespace MusicManager.WpfApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ICommand? _addAlbumCommand = null;
        private ICommand? _editAlbumCommand = null;
        private ICommand? _deleteAlbumCommand = null;

        private ICommand? _addTrackCommand = null;
        private ICommand? _editTrackCommand = null;
        private ICommand? _deleteTrackCommand = null;

        public ICommand AddAlbumCommand
        {
            get
            {
                return RelayCommand.CreateCommand(ref _addAlbumCommand, (p) => AddAlbum(), (p) => true);
            }
        }
        public ICommand EditAlbumCommand
        {
            get
            {
                return RelayCommand.CreateCommand(ref _editAlbumCommand, (p) => EditAlbum(SelectedAlbum!.Id), (p) => SelectedAlbum != null);
            }
        }
        public ICommand DeleteAlbumCommand
        {
            get
            {
                return RelayCommand.CreateCommand(ref _deleteAlbumCommand, (p) => DeleteAlbum(SelectedAlbum!.Id), (p) => SelectedAlbum != null);
            }
        }

        public ICommand AddTrackCommand
        {
            get
            {
                return RelayCommand.CreateCommand(ref _addTrackCommand, (p) => AddTrack(), (p) => true);
            }
        }
        public ICommand EditTrackCommand
        {
            get
            {
                return RelayCommand.CreateCommand(ref _editTrackCommand, (p) => EditTrack(SelectedTrack!.Id), (p) => SelectedTrack != null);
            }
        }
        public ICommand DeleteTrackCommand
        {
            get
            {
                return RelayCommand.CreateCommand(ref _deleteTrackCommand, (p) => DeleteTrack(SelectedTrack!.Id), (p) => SelectedTrack != null);
            }
        }

        private void AddAlbum()
        {
            AlbumWindow window = new AlbumWindow();

            window.ShowDialog();
            OnPropertyChanged(nameof(Albums));
        }
        private void EditAlbum(int id)
        {
            AlbumWindow window = new AlbumWindow();

            if (window.DataContext is AlbumViewModel viewModel)
            {
                viewModel.Model = SelectedAlbum!;
            }
            window.ShowDialog();
            OnPropertyChanged(nameof(Albums));
        }
        private void DeleteAlbum(int id)
        {
            if (MessageBox.Show($"Delete item '{SelectedAlbum!}'?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var repo = new Logic.Repos.AlbumRepository();

                repo.Delete(id);
                repo.SaveChanges();
                OnPropertyChanged(nameof(Albums));
            }
        }

        private void AddTrack()
        {
            AlbumWindow window = new AlbumWindow();

            window.ShowDialog();
            OnPropertyChanged(nameof(Albums));
        }
        private void EditTrack(int id)
        {
            AlbumWindow window = new AlbumWindow();

            if (window.DataContext is AlbumViewModel viewModel)
            {
                viewModel.Model = SelectedAlbum!;
            }
            window.ShowDialog();
            OnPropertyChanged(nameof(Albums));
        }
        private void DeleteTrack(int id)
        {
            if (MessageBox.Show($"Delete item '{SelectedAlbum!}'?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var repo = new Logic.Repos.AlbumRepository();

                repo.Delete(id);
                repo.SaveChanges();
                OnPropertyChanged(nameof(Albums));
            }
        }

        public ObservableCollection<Logic.Models.Album> Albums
        {
            get
            {
                var repo = new Logic.Repos.AlbumRepository();
                var models = repo.GetAll().Where(e => string.IsNullOrEmpty(_searchTextAlbum) || e.ToString().ToLower().Contains(SearchTextAlbum.ToLower()));

                return new ObservableCollection<Logic.Models.Album>(models);
            }
        }
        public ObservableCollection<Logic.Models.Track> Tracks
        {
            get
            {
                var result = default(ObservableCollection<Logic.Models.Track>);

                if (SelectedAlbum != null)
                {
                    var repo = new Logic.Repos.TrackRepository();
                    var models = repo.GetAll().Where(e => e.AlbumId == SelectedAlbum!.Id && string.IsNullOrEmpty(_searchTextTrack) || e.ToString().ToLower().Contains(SearchTextTrack.ToLower()));

                    result = new ObservableCollection<Logic.Models.Track>(models);
                }
                return result ?? new ObservableCollection<Logic.Models.Track>();
            }
        }


        private Logic.Models.Album? _selectedAlbum = null;
        public Logic.Models.Album? SelectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                _selectedAlbum = value;
                OnPropertyChanged(nameof(Tracks));
            }
        }
        private string? _searchTextAlbum;
        public string SearchTextAlbum
        {
            get => _searchTextAlbum ?? string.Empty;
            set
            {
                _searchTextAlbum = value;
                OnPropertyChanged(nameof(Albums));
            }
        }

        private Logic.Models.Track? _selectedTrack = null;
        public Logic.Models.Track? SelectedTrack
        {
            get => _selectedTrack;
            set
            {
                _selectedTrack = value;
            }
        }

        private string? _searchTextTrack;
        public string SearchTextTrack
        {
            get => _searchTextTrack ?? string.Empty;
            set
            {
                _searchTextTrack = value;
                OnPropertyChanged(nameof(Tracks));
            }
        }

    }
}
