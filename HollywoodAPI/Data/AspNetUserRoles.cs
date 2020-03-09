using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HollywoodAPI.Data
{
    public partial class AspNetUserRoles
    {
        [Key]
        public string UserId { get; set; }
        [ForeignKey("RoleId")]
        public string RoleId { get; set; }

        public virtual AspNetRoles Role { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
