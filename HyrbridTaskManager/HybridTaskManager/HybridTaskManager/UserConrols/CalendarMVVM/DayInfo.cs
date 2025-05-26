using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HybridTaskManager.UserConrols.CalendarMVVM
{
    public class DayInfo : INotifyPropertyChanged
    {
        private string _dayName;
        public string DayName
        {
            get => _dayName;
            set { _dayName = value; OnPropertyChanged(); }
        }

        private string _dayNumber;
        public string DayNumber
        {
            get => _dayNumber;
            set { _dayNumber = value; OnPropertyChanged(); }
        }

        public int ColumnIndex { get; set; }
        public DateTime Date { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
