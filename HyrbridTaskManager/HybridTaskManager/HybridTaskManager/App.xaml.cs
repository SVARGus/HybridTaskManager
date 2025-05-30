using System.Configuration;
using System.Data;
using System.Windows;
using HybridTaskManager.LogSystem;

namespace HybridTaskManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            // Здесь можно добавить код для сохранения состояния приложения или выполнения других действий при выходе
            AppLogger.Info("----- Завершение текущей сессии: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " -----");
        }
    }

}
