using Microsoft.AspNet.Identity.EntityFramework;

namespace RobotWebPanel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AspNetUserLogins : IdentityUserLogin
    { 
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
