using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RobotWebPanel.Enums;

namespace RobotWebPanel.Models
{
   
    public partial class ApplicationUser : IdentityUser
    {

        [Display(Name = "Аккаунт создан")]
        public DateTime CreationDateTime { get; set; }

        [Required] //data annotations allow me to define information, 
        [Display(Name = "Доступ до")] //and also the shape field in the table
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy HH:mm:ss PP}")]

        public DateTime AccessDateTime { get; set; }

        [Required]
        [Display(Name = "Уровень доступа")]
        public EaccessType AccessLevel { get; set; }

        public ApplicationUser()
        {
            CreationDateTime = DateTime.Now;
            
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync
                                    (UserManager<AspNetUsers> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this,
                                    DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<AspNetUsers>
    {

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
     
}