using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace n5.webApi.Models
{
    public class PermissionType : EntityBase
    {
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
