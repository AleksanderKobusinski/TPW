using Data;
using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ModelApi _modelApi;

        public ICommand Start { get; set; }
        public ICommand Stop { get; set; }

        public MainWindowViewModel()
        {
            _modelApi = new ModelApi();

            Start = new RelayCommand(() => StartHandler());
            Stop = new RelayCommand(() => StopHandler());
        }

        public int Count
        {
            get
            {
                return _modelApi.Count;
            }
            set
            {
                if (value.Equals(_modelApi.Count))
                    return;

                _modelApi.Count = value;

                RaisePropertyChanged("Count");
            }
        }

        public ObservableCollection<Ball> Ball
        {
            get
            {
                return _modelApi.Balls;
            }
        }

        private void StartHandler()
        {
            _modelApi.RunStartSimulation();
        }

        private void StopHandler()
        {
            _modelApi.RunStopSimulation();
        }
    }
}
