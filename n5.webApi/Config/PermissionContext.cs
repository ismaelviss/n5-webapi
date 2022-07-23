using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using n5.webApi.Config.Mapping;
using n5.webApi.Models;

namespace n5.webApi
{
    public class PermissionContext : DbContext
    {
        private const string inMemory = "Microsoft.EntityFrameworkCore.InMemory";
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionType> PermissionTypes { get; set; }

        public PermissionContext(DbContextOptions<PermissionContext> options) : base(options) 
        {
            if (Database.ProviderName != inMemory)
                Database.Migrate();
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PermissionTypeMap());
            modelBuilder.ApplyConfiguration(new PermissionMap());
            
        }
    }
}
