using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RobotWebPanel.Models
{
    public partial class RobotModel
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }


        public DateTime CreationDate { get; set; }

        public DateTime AccessDate { get; set; }

        public float LastBalance { get; set; }

        public DateTime LastAccessDate { get; set; }

        public long CountStartsProgramm { get; set; }

        public Guid UniqGuid { get; set; }

    }
}