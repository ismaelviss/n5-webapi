using n5.webApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n5.webApi.Test.Utils
{
    public static class Utilities
    {
        public static void InitializeDbForTests(PermissionContext db)
        {
            db.PermissionTypes.AddRange(GetPermissionTypes());
            db.Permissions.AddRange(GetPermissions());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(PermissionContext db)
        {
            db.Permissions.RemoveRange(db.Permissions);
            db.PermissionTypes.RemoveRange(db.PermissionTypes);
            InitializeDbForTests(db);
        }

        public static void Drop(PermissionContext db)
        {
            db.Permissions.RemoveRange(db.Permissions);
            db.PermissionTypes.RemoveRange(db.PermissionTypes);
            db.SaveChanges();
        }

        public static List<PermissionType> GetPermissionTypes()
        {
            return new List<PermissionType>()
            {
                new PermissionType() { Id = 1, Description = "Seguridad"}
            };
        }

        public static List<Permission> GetPermissions()
        {
            return new List<Permission>()
            {
                new Permission() { Id = 1, EmployeeLastName = "Salvatierra", EmployeeName = "Aldo", PermissionDate = DateTime.Parse("2022-07-19"), PermissionTypeId = 1 }
            };
        }
    }
}
