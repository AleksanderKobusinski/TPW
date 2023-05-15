using Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ModelApi _modelApi;
        public AsyncObservableCollection<NotifyBall> BallsList { get; set; }

        public int BallsNumber
        {
            get { return _modelApi.GetBallsNumber(); }
            set
            {
                _modelApi.SetBallsNumber(value);
                OnPropertyChanged();
            }
        }

        public ICommand Start { get; set; }
        public ICommand Stop { get; set; }

        private int OldBallNumber = 10;

        public MainViewModel()
        {
            _modelApi = new ModelApi();
            BallsList = new AsyncObservableCollection<NotifyBall>();
            BallsNumber = OldBallNumber;

            Start = new RelayCommand(() =>
            {
                if (BallsList.Count > 0 && BallsNumber == OldBallNumber)
                {
                    _modelApi.ReRunSimulation();
                }
                else
                {
                    BallsList.Clear();
                    for (int i = 0; i < BallsNumber; i++)
                    {
                        var notifyBall = new NotifyBall();
                        BallsList.Add(notifyBall);
                    }

                    _modelApi.BallMoved += (sender, args) =>
                    {
                        if (BallsList.Count > 0 && BallsList.Count <= BallsNumber)
                        {
                            BallsList[args.Id % BallsNumber].ChangePosition(args.Position);
                            BallsList[args.Id % BallsNumber].ChangeRadius(args.Radius);
                        }
                    };

                    _modelApi.RunStartSimulation();
                    OldBallNumber = BallsNumber;
                }
                ((RelayCommand)Stop).ChangeIsEnable(true);
                ((RelayCommand)Start).ChangeIsEnable(false);

            });

            Stop = new RelayCommand(() =>
            {
                _modelApi.RunStopSimulation();
                ((RelayCommand)Start).ChangeIsEnable(true);
                ((RelayCommand)Stop).ChangeIsEnable(false);
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var args = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, args);
        }
    }
}
