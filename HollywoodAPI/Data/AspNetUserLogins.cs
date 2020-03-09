using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HollywoodAPI.Data
{
    public partial class AspNetUserLogins
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        [Key]
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
