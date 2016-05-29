using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RobotWebPanel.Models;

namespace RobotWebPanel.Controllers.API
{
    public class AccountApiController : ApiController
    {
        private ModelContext _currentContext = new ModelContext();

        // GET: api/AccountApi/5
        public async Task<int> Get(string userName)
        {
            var users = _currentContext.RobotUsers;
            var currentUserInfo = await users.FirstOrDefaultAsync(model => model.UserName == userName);
            if (currentUserInfo == null)
                return 1;
            if (currentUserInfo.AccessDate < DateTime.Now)
                return 2;

            currentUserInfo.LastAccessDate = DateTime.Now;
            currentUserInfo.CountStartsProgramm++;

            _currentContext.Entry(currentUserInfo).State = EntityState.Modified;
            await _currentContext.SaveChangesAsync();

            return 0;
        }

        // GET: api/AccountApi
        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/
        /*
        // GET: api/AccountApi/5
        
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AccountApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AccountApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AccountApi/5
        public void Delete(int id)
        {
        }*/
    }
}
