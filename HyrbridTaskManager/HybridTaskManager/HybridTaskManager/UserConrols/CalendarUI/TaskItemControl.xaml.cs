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


namespace HybridTaskManager.UserConrols.CalendarUI
{
    /// <summary>
    /// Логика взаимодействия для TaskItemControl.xaml
    /// </summary>
    public partial class TaskItemControl : UserControl
    {



        


        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(TaskItemControl));

        


        public static readonly DependencyProperty BackgroundColorProperty =
        DependencyProperty.Register(nameof(BackgroundColor), typeof(Brush), typeof(TaskItemControl));

        public Brush BackgroundColor
        {
            get => (Brush)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public static readonly DependencyProperty PriorityColorProperty =
         DependencyProperty.Register(nameof(PriorityColor), typeof(Brush), typeof(TaskItemControl), new PropertyMetadata(Brushes.Transparent));

        public Brush PriorityColor
        {
            get => (Brush)GetValue(PriorityColorProperty);
            set => SetValue(PriorityColorProperty, value);
        }


        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public TaskItemControl()
        {
            InitializeComponent();
            DataContext = this; // чтобы биндинг сработал на зависимости
        }
    }
}

