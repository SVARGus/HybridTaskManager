using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HybridTaskManager.UserConrols.CalendarMVVM
{
    public class WeekViewModel : INotifyPropertyChanged
    {
        private DateTime _startDate;

        public ObservableCollection<DayInfo> Days { get; } = new ObservableCollection<DayInfo>();

        // Команда для переключения недель
        public ICommand NextWeekCommand { get; }
        public ICommand PreviousWeekCommand { get; }

        public WeekViewModel()
        {
            // Начинаем с текущей недели
            _startDate = GetStartOfWeek(DateTime.Today);
            GenerateDays();

            // Инициализация команд
            NextWeekCommand = new RelayCommand(_ => GoToNextWeek());
            PreviousWeekCommand = new RelayCommand(_ => GoToPreviousWeek());
        }

        private void GenerateDays()
        {
            Days.Clear();
            for (int i = 0; i < 7; i++)
            {
                DateTime day = _startDate.AddDays(i);
                Days.Add(new DayInfo
                {
                    DayName = day.ToString("ddd").ToUpper(),
                    DayNumber = day.Day.ToString(),
                    ColumnIndex = i,
                    Date = day
                });
            }
        }

        private DateTime GetStartOfWeek(DateTime date)
        {
            // Находим ближайший понедельник
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-diff).Date;
        }

        private void GoToNextWeek()
        {
            _startDate = _startDate.AddDays(7);
            GenerateDays();
        }

        private void GoToPreviousWeek()
        {
            _startDate = _startDate.AddDays(-7);
            GenerateDays();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
