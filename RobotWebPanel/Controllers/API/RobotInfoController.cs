using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RobotWebPanel.Models;

namespace RobotWebPanel.Controllers.API
{
    public class RobotInfoController : ApiController
    {
        private ModelContext _context = new ModelContext();
        // GET: api/RobotInfo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/RobotInfo\
        // Update size
        public async void Post([FromBody]float value,[FromBody]string userName)
        {
            var elem = _context.RobotUsers.Where(model => model.UserName == userName);
            if (!elem.Any()) return;
            var firstOrDefault = elem.FirstOrDefault();
            if (firstOrDefault != null) firstOrDefault.LastBalance = value;
            _context.Entry(elem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        // PUT: api/RobotInfo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RobotInfo/5
        public void Delete(int id)
        {
        }
    }
}
