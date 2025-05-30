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
using HybridTaskManager.LogSystem;
using HybridTaskManager.UserConrols.TaskManageControls;
using HybridTaskManager.DataBaseSimulation;
using HybridTaskManager.DTO.DictionaryEntity;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;
using System.Collections.ObjectModel;
using TaskStatus = HybridTaskManager.DTO.DictionaryEntity.TaskStatus;

namespace HybridTaskManager.UserConrols
{
    /// <summary>
    /// Логика взаимодействия для KanbanColumnControl.xaml
    /// </summary>
    public partial class KanbanColumnControl : UserControl
    {
        private User CurrentUser;

        public KanbanColumnControl()
        {
            InitializeComponent();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            AppLogger.Info("Открытие окна создания новой задачи");

            dynamic columnVm = ((FrameworkElement)sender).DataContext;
            TaskStatus status = columnVm.Status;
            ObservableCollection<TaskItem> tasks = columnVm.Tasks;

            var manageTaskControl = new ManageExistingTaskControl(CurrentUser);

            var window = new Window()
            {
                Title = "Новая задача",
                Content = manageTaskControl,
                SizeToContent = SizeToContent.WidthAndHeight,
                Owner = Window.GetWindow(this),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ResizeMode = ResizeMode.NoResize
            };

            if (window.ShowDialog() == true)
            {
                // добавляем задачу в колонку
                if (manageTaskControl.DataContext is TaskItem newTask)
                {
                    columnVm.Tasks.Add(newTask);
                    AppLogger.Info($"Задача добавлена в статус \"{status.Name}\": {newTask.Title}");
                }
            }
        }

        private void RenameColumn_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Переименовать колонку
        }

        private void DeleteColumn_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Удалить текущую колонку
        }

        private void RefreshColumn_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Обновить содержимое колонки
        }

        private void CopyColumn_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Копировать колонку и её задачи
        }
    }
}
