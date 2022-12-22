using System;
using System.Linq;
using System.Windows.Input;
using Logic = Repository.Logic;

namespace MusicManager.WpfApp.ViewModels
{
    public class TrackViewModel : BaseViewModel
    {
        public static string TrackFilePath = @"C:\Temp\track.json";

        private ICommand? _saveCommand = null;
        private ICommand? _closeCommand = null;

        private Logic.Models.Track model = new();
        public Logic.Models.Track Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Composer));
            }
        }
        public Action? CloseWindow { get; set; }
        public ICommand SaveCommand
        {
            get
            {
                return RelayCommand.CreateCommand(ref _saveCommand, (p) => Save());
            }
        }
        public ICommand CloseCommand
        {
            get
            {
                return RelayCommand.CreateCommand(ref _closeCommand, (p) => Close());
            }
        }

        public int AlbumId
        {
            get => Model.AlbumId;
            set
            {
                Model.AlbumId = value;
            }
        }
        public string Name
        {
            get => Model.Name;
            set
            {
                Model.Name = value;
            }
        }
        public string Composer
        {
            get => Model.Composer;
            set
            {
                Model.Composer = value;
            }
        }
        public string Genre
        {
            get => Model.Genre.ToString();
            set
            {
                Model.Genre = (Logic.Models.Genre)Enum.Parse(typeof(Logic.Models.Genre), value);
            }
        }
        public string[] GenreList
        {
            get
            {
                var values = Enum.GetNames(typeof(Logic.Models.Genre));

                return values.OrderBy(v => v).ToArray();
            }
        }
        public void Save()
        {
            using var repo = new Logic.Repos.TrackRepository(TrackFilePath);

            if (Model.Id == 0)
            {
                repo.Add(Model);
            }
            else
            {

                repo.Update(Model);
            }
            repo.SaveChanges();
            Close();
        }
        public void Close()
        {
            CloseWindow?.Invoke();
        }

    }
}
