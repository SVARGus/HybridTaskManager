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
    /// Логика взаимодействия для KanbanTaskCardControl.xaml
    /// </summary>
    public partial class KanbanTaskCardControl : UserControl
    {
        public KanbanTaskCardControl()
        {
            InitializeComponent();
        }

        private void OpenTask_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Открыть карточку задачи в отдельном окне или области
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Удалить текущую задачу
        }

        private void CopyTask_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Копировать данные задачи
        }

        private void MoveTask_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Переместить задачу в другую колонку
        }
    }
}
