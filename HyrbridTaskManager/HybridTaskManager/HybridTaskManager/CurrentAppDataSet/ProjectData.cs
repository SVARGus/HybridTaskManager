using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;

namespace HybridTaskManager.DataBaseSimulation
{
    public static class ProjectData
    {
        public static ObservableCollection<Project> Projects { get; private set; }

        static ProjectData()
        {
            LoadFromJson(@"..\..\..\LocalSaveDataManage\Config\ProjectDataConfig.json");
        }

        private static void LoadFromJson(string path)
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var projects = JsonSerializer.Deserialize<ObservableCollection<Project>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                Projects = projects ?? new ObservableCollection<Project>();
            }
            else
            {
                Projects = new ObservableCollection<Project>();
            }
        }
    }
}
