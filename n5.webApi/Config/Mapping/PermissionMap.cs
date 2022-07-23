using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using n5.webApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n5.webApi.Config.Mapping
{
    public class PermissionMap : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permisos");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasComment("Unique ID");
            builder.Property(p => p.EmployeeName).IsRequired().HasMaxLength(200).HasColumnName("NombreEmpleado").HasComment("Employee Forename");
            builder.Property(p => p.EmployeeLastName).IsRequired().HasMaxLength(200).HasColumnName("ApellidoEmpleado").HasComment("Employee Surname");
            
            builder.Property(p => p.PermissionDate).IsRequired().HasColumnName("FechaPermiso").HasComment("Permission granted on Date");

            builder.HasOne(x => x.PermissionType).WithMany(x => x.Permissions).HasForeignKey(p => p.PermissionTypeId);

            //var permissions = new List<Permission>();
            //permissions.Add(new Permission() { Id = 1, EmployeeLastName= "Salvatierra", EmployeeName="Elvis", PermissionDate = new System.DateTime(), PermissionTypeId = 1, PermissionType = new PermissionType() { Id = 1, Description = "Seguridad" }});
            //builder.HasData(permissions);
        }
    }
}
