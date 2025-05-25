using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridTaskManager.DTO.ProjectsAndProjectRoles.UserEntity;

namespace HybridTaskManager.DataBaseSimulation
{
    public static class UserRoleDataBase
    {
        public static ObservableCollection<UserRole> userRoles = new ObservableCollection<UserRole>
        {
            new UserRole("Разработчик",false,true),
            new UserRole("Дизайнер",false,true) 
        };
    }
}
