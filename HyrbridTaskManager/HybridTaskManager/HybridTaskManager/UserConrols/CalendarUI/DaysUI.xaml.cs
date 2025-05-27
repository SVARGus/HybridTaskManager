using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HybridTaskManager.DTO.DictionaryEntity;

namespace HybridTaskManager.UserConrols.CalendarUI
{
    /// <summary>
    /// Логика взаимодействия для DaysUI.xaml
    /// </summary>
    public partial class DaysUI : UserControl
    {
        public DaysUI()
        {
            InitializeComponent();
            SetDateInfo();
        }

        public void SetDateInfo(DateTime? date = null)
        {
            DateTime currentDate = date ?? DateTime.Now;

            DayOfWeekText.Text = currentDate.ToString("dddd"); // e.g., Monday
            DayNumberText.Text = currentDate.Day.ToString();    // e.g., 27
            MonthText.Text = currentDate.ToString("MMMM");     // e.g., May
        }
    }
}
