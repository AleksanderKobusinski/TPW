using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly SimulationViewModel _simulationViewModel;

        public ICommand Start { get; set; }
        public ICommand Stop { get; set; }

        public MainWindowViewModel()
        {
            _simulationViewModel = new SimulationViewModel();
            
            Start = new RelayCommand(() => StartHandler());
            Stop = new RelayCommand(() => StopHandler());
        }

        public int Count
        {
            get
            {
                return _simulationViewModel.Count;
            }
            set
            {
                if (value.Equals(_simulationViewModel.Count))
                    return;

                _simulationViewModel.Count = value;

                RaisePropertyChanged("Count");
            }
        }

        public ObservableCollection<Ball> Ball
        {
            get
            {
                return _simulationViewModel.Balls;
            }
        }

        private void StartHandler()
        {
            _simulationViewModel.RunStartSimulation();
        }

        private void StopHandler()
        {
            _simulationViewModel.RunStopSimulation();
        }
    }
}
