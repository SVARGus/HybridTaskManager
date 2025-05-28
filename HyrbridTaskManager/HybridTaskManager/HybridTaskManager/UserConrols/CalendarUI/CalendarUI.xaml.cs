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
        private DateTime currentWeekStart;

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
                    .Where(task =>
                        task.StartAt.Date <= weekEnd.Date &&  // началась до конца недели
                        task.DeadLine.Date >= weekStart.Date  // закончится после начала недели
                    )
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

        private (int? startCol, int? endCol) ClampToVisibleWeek(DateTime weekStart, DateTime taskStart, DateTime taskEnd)
        {
            DateTime weekEnd = weekStart.AddDays(6);

            
            if (taskEnd < weekStart || taskStart > weekEnd)
                return (null, null);

            
            int startCol = Math.Max(0, (taskStart.Date - weekStart).Days);
            int endCol = Math.Min(6, (taskEnd.Date - weekStart).Days);

            return (startCol, endCol);
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

                var (startCol, endCol) = ClampToVisibleWeek(weekStart, task.StartAt.Date, task.DeadLine.Date);
                if (startCol == null || endCol == null)
                    continue;

                int row = FindAvailableRow(placed, startCol.Value, endCol.Value);

                var brushPriority = (SolidColorBrush)(new BrushConverter().ConvertFromString(task.Priority.HexColorCode) ?? Brushes.Transparent);

                string hexColor = TaskColorHelper.GetColorByIndex(i);
                var brush = (SolidColorBrush)(new BrushConverter().ConvertFromString(hexColor) ?? Brushes.LightGray);

                var tooltip = new ToolTip
                {
                    Background = Brushes.White,
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1),
                    Padding = new Thickness(10),
                    Content = new StackPanel
                    {
                        Children =
                {
                    new TextBlock
                    {
                        Text = task.Title,
                        FontWeight = FontWeights.Bold,
                        FontSize = 14,
                        Margin = new Thickness(0, 0, 0, 5)
                    },
                    new TextBlock
                    {
                        Text = task.Description,
                        TextWrapping = TextWrapping.Wrap,
                        Margin = new Thickness(0, 0, 0, 5)
                    },
                    new TextBlock
                    {
                        Text = $"Сроки: {task.StartAt:dd.MM.yyyy} — {task.DeadLine:dd.MM.yyyy}",
                        Foreground = Brushes.DimGray
                    },
                    new TextBlock
                    {
                        Text = $"Приоритет: {task.Priority?.Name ?? "—"}",
                        Foreground = Brushes.DimGray
                    },
                    new TextBlock
                    {
                        Text = $"Статус: {task.Status?.Name ?? "—"}",
                        Foreground = Brushes.DimGray
                    },
                    new TextBlock
                    {
                        Text = $"Ответственный: {task.AssignedTo?.UserName ?? "—"}",
                        Foreground = Brushes.DimGray
                    }
                }
                    }
                };

                var taskControl = new TaskItemControl
                {
                    Title = task.Title,
                    ToolTip = tooltip,
                    PriorityColor = brushPriority,
                    BackgroundColor = brush
                };

                Grid.SetRow(taskControl, row);
                Grid.SetColumn(taskControl, startCol.Value);
                Grid.SetColumnSpan(taskControl, endCol.Value - startCol.Value + 1);

                grid.Children.Add(taskControl);
                placed.Add((startCol.Value, endCol.Value, row));
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

            currentWeekStart = GetStartOfWeek(DateTime.Today);

            SetupWeekDays();
            FillTasksGrid(currentWeekStart);
            UpdateWeekLabel();

            foreach (var btn in FindVisualChildren<Button>(ButtonBorder))
            {
                btn.Click += Btn_Click;
            }
        }

        private void SetupWeekDays()
        {
            var dayGrid = DayBorder.Child as Grid;
            if (dayGrid == null) return;

            for (int col = 0; col < 7; col++)
            {
                DateTime dayDate = currentWeekStart.AddDays(col);
                if (GetChildByColumn(dayGrid, col) is DaysUI dayUI)
                {
                    dayUI.SetDateInfo(dayDate);
                }
            }
        }

        private void PrevWeek_Click(object sender, RoutedEventArgs e)
        {
            currentWeekStart = currentWeekStart.AddDays(-7);
            SetupWeekDays();
            FillTasksGrid(currentWeekStart);
            UpdateWeekLabel();
        }

        private void NextWeek_Click(object sender, RoutedEventArgs e)
        {
            currentWeekStart = currentWeekStart.AddDays(7);
            SetupWeekDays();
            FillTasksGrid(currentWeekStart);
            UpdateWeekLabel();
        }

        private void UpdateWeekLabel()
        {
            WeekLabel.Text = $"{currentWeekStart:dd MMM} - {currentWeekStart.AddDays(6):dd MMM yyyy}";
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
