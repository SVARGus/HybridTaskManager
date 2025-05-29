using System.Collections.ObjectModel;
using HybridTaskManager.DTO.ProjectsAndProjectRoles;
using HybridTaskManager.LocalSaveDataManage;
using HybridTaskManager.LocalSaveDataManage.SaveDataSerialize;

namespace HybridTaskManager.DataBaseSimulation
{
    public static class ProjectRoleData
    {
        public static JsonRepository<ProjectRole> Repository { get; private set; }

        public static ObservableCollection<ProjectRole> Roles => Repository.Items;

        static ProjectRoleData()
        {
            Repository = new JsonRepository<ProjectRole>("LocalSaveDataManage/Config/TaskDataConfig.json");
        }

        public static void Save() => Repository.Save();
    }
}
