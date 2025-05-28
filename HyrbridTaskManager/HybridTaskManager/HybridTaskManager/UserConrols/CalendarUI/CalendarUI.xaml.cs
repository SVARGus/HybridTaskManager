using HybridTaskManager.DataBaseSimulation;
using HybridTaskManager.DTO.DictionaryEntity;
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

        private List<TaskItem> GetTasksForWeek(DateTime weekStart)
        {
            DateTime weekEnd = weekStart.AddDays(6);
            try
            {
                return TaskDataBase.TaskBase
                    .Where(task => task.StartAt.Date >= weekStart.Date && task.StartAt.Date <= weekEnd.Date)
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}\n\nStackTrace:\n{ex.StackTrace}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<TaskItem>();
            }
        }

        private int FindAvailableRow(List<(int startCol, int endCol, int row)> placed, int start, int end)
        {
            for (int row = 0; row < 8; row++)
            {
                bool intersects = placed.Any(p =>
                    p.row == row && !(p.endCol < start || p.startCol > end));

                if (!intersects)
                    return row;
            }

            return 7; 
        }


        private void FillTasksGrid(DateTime weekStart)
        {
            var grid = CalendarBorder.Child as Grid;
            if (grid == null) return;
            grid.Children.Clear();

            var tasks = GetTasksForWeek(weekStart);
            var placed = new List<(int startCol, int endCol, int row)>(); 

            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];

                int startCol = (task.StartAt.Date - weekStart.Date).Days;
                int endCol = (task.DeadLine.Date - weekStart.Date).Days;

                if (startCol < 0 || startCol > 6) continue;
                if (endCol < startCol) endCol = startCol;
                if (endCol > 6) endCol = 6;

                int row = FindAvailableRow(placed, startCol, endCol);

                var taskControl = new TaskItemControl
                {
                    Title = task.Title,
                    ToolTip = $"{task.Title}\n{task.Description}\n{task.StartAt} - {task.DeadLine}"
                };

                
                string hexColor = TaskColorHelper.GetColorByIndex(i);
                var brush = (SolidColorBrush)(new BrushConverter().ConvertFromString(hexColor) ?? Brushes.LightGray);
                taskControl.BackgroundColor = brush;

                Grid.SetRow(taskControl, row);
                Grid.SetColumn(taskControl, startCol);
                Grid.SetColumnSpan(taskControl, endCol - startCol + 1);

                grid.Children.Add(taskControl);
                placed.Add((startCol, endCol, row));

            }
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

        private DateTime GetStartOfWeek(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-diff).Date;
        }

        private void CalendarUI_Loaded(object sender, RoutedEventArgs e)
        {
            if (isInitialized) return;
            isInitialized = true;

            SetupWeekDays();

            DateTime weekStart = GetStartOfWeek(DateTime.Today);
            FillTasksGrid(weekStart);


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
