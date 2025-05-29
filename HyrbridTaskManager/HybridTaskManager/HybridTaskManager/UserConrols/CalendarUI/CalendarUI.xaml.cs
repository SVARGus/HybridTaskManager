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
   
    public partial class CalendarUI : UserControl
    {

        private bool isInitialized = false;
        private DateTime currentWeekStart;

        public CalendarUI()
        {
            InitializeComponent();
            Loaded += CalendarUI_Loaded;

        }


        /// <summary>
        /// Возвращает список задач, пересекающих интервал текущей недели.
        /// Учитываются те задачи, у которых StartAt ≤ конец недели и DeadLine ≥ начало недели.
        /// </summary>
        private List<TaskItem> GetTasksForWeek(DateTime weekStart)
        {
            DateTime weekEnd = weekStart.AddDays(6);
            try
            {
                return TaskDataBase.TaskBase
                    .Where(task =>
                        task.StartAt.Date <= weekEnd.Date &&  
                        task.DeadLine.Date >= weekStart.Date  
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}\n\nStackTrace:\n{ex.StackTrace}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<TaskItem>();
            }
        }

        /// <summary>
        /// Ищет первый свободный ряд, где новый прямоугольник (задача) не пересечётся с уже размещёнными.
        /// </summary>

        private int FindAvailableRow(List<(int startCol, int endCol, int row)> placed, int start, int end)
        {
            for (int row = 0; row < 8; row++)
            {
                bool intersects = placed.Any(p =>
                    p.row == row && !(p.endCol < start || p.startCol > end));

                if (!intersects)
                    return row;
            }
            // Если все занято, возвращаем последний ряд
            return 7; 
        }

        /// <summary>
        /// Обрезает диапазон колонки (startCol..endCol) по границам видимой недели [0..6].
        /// Если задача полностью вне недели, возвращает (null, null).
        /// </summary>

        private (int? startCol, int? endCol) ClampToVisibleWeek(DateTime weekStart, DateTime taskStart, DateTime taskEnd)
        {
            DateTime weekEnd = weekStart.AddDays(6);

            
            if (taskEnd < weekStart || taskStart > weekEnd)
                return (null, null);

            
            int startCol = Math.Max(0, (taskStart.Date - weekStart).Days);
            int endCol = Math.Min(6, (taskEnd.Date - weekStart).Days);

            return (startCol, endCol);
        }

        /// <summary>
        /// Запрашивает подтверждение удаления и при согласии удаляет задачу из базы и перерисовывает календарь.
        /// </summary>
        private void TryDeleteTask(TaskItem task)
        {
            var result = MessageBox.Show(
                $"Вы уверены, что хотите удалить задачу:\n\n«{task.Title}»?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                TaskDataBase.TaskBase.Remove(task); 
                FillTasksGrid(currentWeekStart);    
            }
        }


        /// <summary>
        /// Создает для каждой задачи свой TaskItemControl, расставляет их в гриде в нужных строках и колонках,
        /// а также навешивает события клика и правого клика.
        /// </summary>
        private void FillTasksGrid(DateTime weekStart)
        {
            var scrollViewer = CalendarBorder.Child as ScrollViewer;
            var grid = TaskGrid;
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

                taskControl.MouseLeftButtonUp += (s, e) =>
                {
                    ShowTaskEditor(task);
                    e.Handled = true;
                };

                taskControl.MouseRightButtonUp += (s, e) =>
                {
                    TryDeleteTask(task);
                    e.Handled = true;
                };

                grid.Children.Add(taskControl);
                placed.Add((startCol.Value, endCol.Value, row));
            }
        }

        // <summary>
        /// Открывает окно редактирования/создания задачи и после закрытия обновляет календарь.
        /// </summary>
        private void ShowTaskEditor(TaskItem task)
        {
           
            var manageTaskControl = new ManageExistingTaskControl(task, null);

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

            FillTasksGrid(currentWeekStart);
        }



        /// <summary>
        /// Возвращает дату начала недели (понедельник) для переданной даты.
        /// </summary>

        private DateTime GetStartOfWeek(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-diff).Date;
        }


        /// <summary>
        /// При загрузке контрола — инициализируем отображение и заполняем задачи.
        /// </summary>

        private void CalendarUI_Loaded(object sender, RoutedEventArgs e)
        {
            if (isInitialized) return;
            isInitialized = true;

            currentWeekStart = GetStartOfWeek(DateTime.Today);

            SetupWeekDays();
            FillTasksGrid(currentWeekStart);
            UpdateWeekLabel();

        }

        /// <summary>
        /// Обработчик нажатия кнопки создания новой задачи.
        /// </summary>

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var manageTaskControl = new ManageExistingTaskControl(UserDataBase.Users[0]);

            var window = new Window()
            {
                Title = "Новая задача",
                Content = manageTaskControl,
                SizeToContent = SizeToContent.WidthAndHeight,
                Owner = Window.GetWindow(this),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ResizeMode = ResizeMode.NoResize
            };

            window.ShowDialog();

            FillTasksGrid(currentWeekStart);
        }

        /// <summary>
        /// Устанавливает подписи (день недели и дату) во всех семи днях.
        /// </summary>

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

        /// <summary>
        /// Переходит на предыдущую неделю.
        /// </summary>

        private void PrevWeek_Click(object sender, RoutedEventArgs e)
        {
            currentWeekStart = currentWeekStart.AddDays(-7);
            SetupWeekDays();
            FillTasksGrid(currentWeekStart);
            UpdateWeekLabel();
        }

        /// <summary>
        /// Переходит на следующую неделю.
        /// </summary>
        private void NextWeek_Click(object sender, RoutedEventArgs e)
        {
            currentWeekStart = currentWeekStart.AddDays(7);
            SetupWeekDays();
            FillTasksGrid(currentWeekStart);
            UpdateWeekLabel();
        }

        /// <summary>
        /// Обновляет заголовок текущей недели (диапазон дат).
        /// </summary>

        private void UpdateWeekLabel()
        {
            WeekLabel.Text = $"{currentWeekStart:dd MMM} - {currentWeekStart.AddDays(6):dd MMM yyyy}";
        }

        /// <summary>
        /// Вспомогательный: находит элемент в гриде по колонке (для SetupWeekDays).
        /// </summary>
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
