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

namespace HybridTaskManager.UserConrols
{
    /// <summary>
    /// Логика взаимодействия для KanbanColumnControl.xaml
    /// </summary>
    public partial class KanbanColumnControl : UserControl
    {
        public KanbanColumnControl()
        {
            InitializeComponent();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Добавить новую задачу в эту колонку
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
