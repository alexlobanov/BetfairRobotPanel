using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace RobotWebPanel.Models
{
    public class BetfairDbInitializer : DropCreateDatabaseIfModelChanges<ModelContext>
    {

        protected override void Seed(ModelContext db)
        {
            base.Seed(db);
        }
    }
}