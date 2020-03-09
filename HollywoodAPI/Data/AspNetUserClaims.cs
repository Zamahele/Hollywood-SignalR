using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HollywoodAPI.Data
{
    public partial class AspNetUserClaims
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
