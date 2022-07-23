using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace n5.webApi.Models
{
    public class Permission : EntityBase
    {
        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public string EmployeeLastName { get; set; }
        
        [Required]
        public int PermissionTypeId { get; set; }
        public virtual PermissionType PermissionType { get; set; }
        
        [Required]
        public DateTime PermissionDate { get; set; }

    }
}