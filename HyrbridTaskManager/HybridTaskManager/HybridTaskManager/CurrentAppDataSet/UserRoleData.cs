using System.Collections.ObjectModel;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;
using HybridTaskManager.LocalSaveDataManage;
using HybridTaskManager.LocalSaveDataManage.SaveDataSerialize;

namespace HybridTaskManager.DataBaseSimulation
{
    public static class UserRoleData
    {
        public static JsonRepository<UserRole> Repository { get; private set; }

        public static ObservableCollection<UserRole> UserRoles => Repository.Items;

        static UserRoleData()
        {
            Repository = new JsonRepository<UserRole>("LocalSaveDataManage/Config/TaskDataConfig.json");
        }

        public static void Save() => Repository.Save();
    }
}
