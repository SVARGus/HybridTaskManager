using HybridTaskManager.DataBaseSimulation;
using HybridTaskManager.UserConrols.TaskManageControls;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace HybridTaskManager.UserConrols.CalendarUI
{
    /// <summary>
    /// Логика взаимодействия для CalendarUI.xaml
    /// </summary>
    public partial class CalendarUI : UserControl
    {

        private bool isInitialized = false;

        public CalendarUI()
        {
            InitializeComponent();
            
            Loaded += CalendarUI_Loaded;

        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            

            var manageTaskControl = new ManageExistingTaskControl(UserDataBase.Users[0]);

            var window = new Window()
            {
                Title = "Редактирование задачи",
                Content = manageTaskControl,
                SizeToContent = SizeToContent.WidthAndHeight,
                Owner = Window.GetWindow(this), 
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ResizeMode = ResizeMode.NoResize
            };

            window.ShowDialog();
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T t)
                    yield return t;

                foreach (T childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }

        private void CalendarUI_Loaded(object sender, RoutedEventArgs e)
        {
            if (isInitialized) return;
            isInitialized = true;

            SetupWeekDays();

            foreach (var btn in FindVisualChildren<Button>(ButtonBorder))
            {
                btn.Click += Btn_Click;
            }
        }

        private void SetupWeekDays()
        {
            DateTime today = DateTime.Today;
            int currentDayIndex = (int)today.DayOfWeek;
            currentDayIndex = currentDayIndex == 0 ? 7 : currentDayIndex; 

            
            var dayGrid = DayBorder.Child as Grid;
            if (dayGrid == null) return;

            for (int col = 0; col < 7; col++)
            {
                DateTime dayDate = today.AddDays(col - currentDayIndex + 1);

                if (GetChildByColumn(dayGrid, col) is DaysUI dayUI)
                {
                    dayUI.SetDateInfo(dayDate);
                }
            }
        }

        private UIElement GetChildByColumn(Grid grid, int column)
        {
            foreach (UIElement child in grid.Children)
            {
                if (Grid.GetColumn(child) == column)
                    return child;
            }
            return null;
        }
    }
}
