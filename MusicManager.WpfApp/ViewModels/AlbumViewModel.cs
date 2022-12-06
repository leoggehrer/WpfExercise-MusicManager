using System;
using System.Windows.Input;
using Logic = Repository.Logic;

namespace MusicManager.WpfApp.ViewModels
{
    public class AlbumViewModel : BaseViewModel
    {
        private ICommand? _saveCommand = null;
        private ICommand? _closeCommand = null;

        private Logic.Models.Album model = new();
        public Logic.Models.Album Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(Interpreter));
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

        public string Title
        {
            get => Model.Title;
            set
            {
                Model.Title = value;
            }
        }
        public string Interpreter
        {
            get => Model.Interpreter;
            set
            {
                Model.Interpreter = value;
            }
        }

        public void Save()
        {
            using var repo = new Logic.Repos.AlbumRepository();

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
