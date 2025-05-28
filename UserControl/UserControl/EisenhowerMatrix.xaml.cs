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

namespace HybridTaskManager.UserControl
{
    /// <summary>
    /// Логика взаимодействия для EisenhowerMatrix.xaml
    /// </summary>
    public partial class EisenhowerMatrix
    {
        public EisenhowerMatrix()
        {
            InitializeComponent();
        }

        private void AddUrgentImportantTask_Click(object sender, RoutedEventArgs e)
        {
            string task = UrgentImportantInput.Text.Trim();
            if (!string.IsNullOrEmpty(task))
            {
                UrgentImportantTasks.Items.Add(task);
                UrgentImportantInput.Clear();
            }
        }

        private void AddNotUrgentImportantTask_Click(object sender, RoutedEventArgs e)
        {
           

            string task = NotUrgentImportantInput.Text.Trim();
            if (!string.IsNullOrEmpty(task))
            {
                NotUrgentImportantTasks.Items.Add(task);
                NotUrgentImportantInput.Clear();
            }
        }

        private void AddUrgentNotImportantTask_Click(object sender, RoutedEventArgs e)
        {
            string task = UrgentNotImportantInput.Text.Trim();
            if (!string.IsNullOrEmpty(task))
            {
                UrgentNotImportantTasks.Items.Add(task);
                UrgentNotImportantInput.Clear();
            }
        }

        private void AddNotUrgentNotImportantTask_Click(object sender, RoutedEventArgs e)
        {
            string task = NotUrgentNotImportantInput.Text.Trim();
            if (!string.IsNullOrEmpty(task))
            {
                NotUrgentNotImportantTasks.Items.Add(task);
                NotUrgentNotImportantInput.Clear();
            }
        }
    }
}
