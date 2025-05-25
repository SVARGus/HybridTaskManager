using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;

namespace HybridTaskManager.DataBaseSimulation
{
    public static class UserDataBase
    {
        public static ObservableCollection<User> Users = new ObservableCollection<User>
        {
            new User("Вася Пупкин",UserRoleDataBase.userRoles[0]),
            new User("Серёжа Валенков",UserRoleDataBase.userRoles[0]),
            new User("Гриша Ламер",UserRoleDataBase.userRoles[0]),
        };
    }
}
