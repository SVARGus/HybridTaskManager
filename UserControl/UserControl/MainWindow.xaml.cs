using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HybridTaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadTasks();
        }

        private void LoadTasks()
        {
            // Пример загрузки задач (вместо этого можно использовать реальную базу данных или API)
            EisenhowerMatrixControl.UrgentImportantTasks.Items.Add("Задача 1");
            EisenhowerMatrixControl.NotUrgentImportantTasks.Items.Add("Задача 2");
            EisenhowerMatrixControl.UrgentNotImportantTasks.Items.Add("Задача 3");
            EisenhowerMatrixControl.NotUrgentNotImportantTasks.Items.Add("Задача 4");
        }
    }

}