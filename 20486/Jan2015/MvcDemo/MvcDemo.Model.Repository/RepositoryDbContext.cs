using MvcDemo.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Model.Repository
{
    public class RepositoryDbContext: DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<AuditableEntity> AuditableEntity { get; set; }
        public DbSet<UserAccount> Account { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Permission> Permission { get; set; }

        public RepositoryDbContext()
            : base("MvcDemoConnection")
        {
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            Configuration.ValidateOnSaveEnabled = true;
        }
        protected override void Dispose(bool disposing)
        {
            Configuration.LazyLoadingEnabled = false;
            base.Dispose(disposing);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.UserAccounts)
                .Map(m =>
                {
                    m.ToTable("UserAccountRoles");
                    m.MapLeftKey("Id");
                    m.MapRightKey("RoleId");
                });

            modelBuilder.Entity<UserAccount>()
                .HasMany(u => u.Permissions)
                .WithMany(r => r.UserAccounts)
                .Map(m =>
                {
                    m.ToTable("UserAccountPermissions");
                    m.MapLeftKey("Id");
                    m.MapRightKey("PermissionId");
                });
        }
    }
}
