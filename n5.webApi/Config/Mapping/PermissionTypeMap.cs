using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using n5.webApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n5.webApi.Config.Mapping
{
    public class PermissionTypeMap : IEntityTypeConfiguration<PermissionType>
    {
        public void Configure(EntityTypeBuilder<PermissionType> builder)
        {
            builder.ToTable("TipoPermisos");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasComment("Unique ID");
            builder.Property(p => p.Description).IsRequired().HasColumnName("Descripcion").HasComment("Permission description");

            builder.HasMany(x => x.Permissions).WithOne(x => x.PermissionType).HasForeignKey(x => x.PermissionTypeId);

            var permissionTypes = new List<PermissionType>();
            permissionTypes.Add(new PermissionType() { Id = 1, Description = "Seguridad" });
            permissionTypes.Add(new PermissionType() { Id = 2, Description = "Medios tecnologicos" });
            permissionTypes.Add(new PermissionType() { Id = 3, Description = "Contabilidad" });
            permissionTypes.Add(new PermissionType() { Id = 4, Description = "Administraciond" });
            
            builder.HasData(permissionTypes);
        }
    }
}
