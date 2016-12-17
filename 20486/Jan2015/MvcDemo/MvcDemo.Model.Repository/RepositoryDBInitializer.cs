using MvcDemo.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Model.Repository
{
    public class RepositoryDBInitializer : CreateDatabaseIfNotExists<RepositoryDbContext>
    {
        protected override void Seed(RepositoryDbContext context)
        {
            Role role1 = new Role { RoleName = "Admin" };
            Role role2 = new Role { RoleName = "User" };

            Permission permission1 = new Permission
            {
                PermissionName = "Read",
                Controller = "Cliente",
                Action = "Index"
            };
            Permission permission2 = new Permission
            {
                PermissionName = "Write",
                Controller = "Cliente",
                Action = "Create"
            };

            UserAccount user1 = new UserAccount
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                FirstName = "Admin",
                Password = "123456",
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "Seed",
                Roles = new List<Role>(),
                Permissions = new List<Permission>()
            };

            UserAccount user2 = new UserAccount 
            {
                UserName = "user1",
                Email = "user1@gmail.com",
                FirstName = "User1",
                Password = "123456",
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "Seed",
                Roles = new List<Role>(),
                Permissions = new List<Permission>()
            };

            user1.Roles.Add(role1);
            user1.Permissions.Add(permission1);
            user1.Permissions.Add(permission2);

            user2.Roles.Add(role2);
            user2.Permissions.Add(permission1);

            context.Account.Add(user1);
            context.Account.Add(user2);

            context.SaveChanges();
        }
    }
}
