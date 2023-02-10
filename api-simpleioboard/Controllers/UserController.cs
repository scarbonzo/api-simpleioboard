using api_simpleioboard.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace api_simpleioboard.Controllers
{
    public class UserController : Controller
    {
        // GET: UserController
        [Route("api/user")]
        [HttpGet]
        public ActionResult Index()
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colUsers = database.GetCollection<User>(configuration.userscollection);

            var result = colUsers
                .AsQueryable()
                .Where(u => u.Enabled == 1)
                .ToList();

            return Ok(result);
        }

        // GET: api/user/reodice
        [Route("api/user/{username}")]
        [HttpGet]
        public ActionResult Details(string username)
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colUsers = database.GetCollection<User>(configuration.userscollection);

            var result = colUsers
                .AsQueryable()
                .Where(u => u.Username == username)
                .FirstOrDefault();

            return Ok(result);
        }

        // GET: api/group/Core
        [Route("api/group/{group}")]
        [HttpGet]
        public ActionResult Group(string group)
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colUsers = database.GetCollection<User>(configuration.userscollection);

            var result = colUsers
                .AsQueryable()
                .Where(u => u.Group == group)
                .ToList();

            return Ok(result);
        }

        // GET: api/user/create
        [Route("api/user/create")]
        [HttpGet]
        public ActionResult Create(
            string username = "reodice",
            string firstname = "Rich",
            string group = "Core",
            string mondayStatus = "WFH",
            string tuesdayStatus = "WFH",
            string wednesdayStatus = "Office",
            string thursdayStatus = "WFH",
            string fridayStatus = "WFH",
            string mondayHours = "8am-4pm",
            string tuesdayHours = "8am-4pm",
            string wednesdayHours = "8am-4pm",
            string thursdayHours = "8am-4pm",
            string fridayHours = "8am-4pm",
            int enabled = 1)
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colUsers = database.GetCollection<User>(configuration.userscollection);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                FirstName = firstname,
                Group = group,
                MondayStatus = mondayStatus,
                TuesdayStatus = tuesdayStatus,
                WednesdayStatus = wednesdayStatus,
                ThursdayStatus = thursdayStatus,
                FridayStatus = fridayStatus,
                MondayHours = mondayHours,
                TuesdayHours = tuesdayHours,
                WednesdayHours = wednesdayHours,
                ThursdayHours = thursdayHours,
                FridayHours = fridayHours,
                Enabled = enabled
            };

            colUsers.InsertOne(user);

            return Ok(user);
        }

        // GET: api/user/update
        [Route("api/user/update")]
        [HttpGet]
        public ActionResult Update(string username, string firstname, string group,
            string mondayStatus, string tuesdayStatus, string wednesdayStatus, string thursdayStatus, string fridayStatus,
            string mondayHours, string tuesdayHours, string wednesdayHours, string thursdayHours, string fridayHours,
            int? enabled)
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colUsers = database.GetCollection<User>(configuration.userscollection);

            try
            {
                var result = colUsers
                .AsQueryable()
                .Where(u => u.Username == username)
                .FirstOrDefault();

                if (result == null)
                {
                    return NotFound("User not found!");
                }
                else
                {
                    var updateFilter = Builders<User>.Filter.Eq("Username", username);
                    var updateList = new List<UpdateDefinition<User>>();

                    if (firstname != null)
                    {
                        updateList.Add(Builders<User>.Update.Set(u => u.FirstName, firstname));
                    }
                    if (group != null)
                    {
                        updateList.Add(Builders<User>.Update.Set(u => u.Group, group));
                    }
                    if (mondayStatus != null)
                    {
                        updateList.Add(Builders<User>.Update.Set(u => u.MondayStatus, mondayStatus));
                    }
                    if (mondayHours != null)
                    {
                        updateList.Add(Builders<User>.Update.Set(u => u.MondayHours, mondayHours));
                    }
                    if (tuesdayStatus != null)
                    {
                        updateList.Add(Builders<User>.Update.Set(u => u.TuesdayStatus, tuesdayStatus));
                    }
                    if (tuesdayHours != null)
                    {
                        updateList.Add(Builders<User>.Update.Set(u => u.TuesdayHours, tuesdayHours));
                    }
                    if (wednesdayStatus != null)
                    {
                        updateList.Add(Builders<User>.Update.Set(u => u.WednesdayStatus, wednesdayStatus));
                    }
                    if (wednesdayHours != null)
                    {
                        updateList.Add(Builders<User>.Update.Set(u => u.WednesdayHours, wednesdayHours));
                    }
                    if (thursdayStatus != null)
                    { 
                        updateList.Add(Builders<User>.Update.Set(u => u.ThursdayStatus, thursdayStatus));
                    }
                    if (thursdayHours != null)
                    {
                        updateList.Add(Builders<User>.Update.Set(u => u.ThursdayHours, thursdayHours));
                    }
                    if (fridayStatus != null)
                    {
                        updateList.Add(Builders<User>.Update.Set(u => u.FridayStatus, fridayStatus));
                    }
                    if (fridayHours != null)
                    {
                        updateList.Add(Builders<User>.Update.Set(u => u.FridayHours, fridayHours));
                    }
                    if (enabled != null)
                    {
                        updateList.Add(Builders<User>.Update.Set("Enabled", enabled));
                    }

                    var update = Builders<User>.Update.Combine(updateList);

                    colUsers.UpdateOne(updateFilter, update);

                    var test = colUsers.Find(updateFilter).FirstOrDefault();

                    return Ok(test);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/user/reodice
        [Route("api/user/delete/{username}")]
        [HttpGet]
        public ActionResult Delete(string username)
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colUsers = database.GetCollection<User>(configuration.userscollection);

            try
            {
                var deleteFilter = Builders<User>.Filter.Eq("Username", username);
                colUsers.DeleteOne(deleteFilter);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(username + " deleted successfully");
        }

        // GET: api/statuses
        [Route("api/statuses")]
        [HttpGet]
        public ActionResult GetStatuses()
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colStatuses = database.GetCollection<Status>(configuration.statusescollection);

            var result = colStatuses
                .AsQueryable()
                .Where(s => s.Enabled)
                .ToList();

            return Ok(result);
        }

        // GET: api/statuses/create
        [Route("api/statuses/create")]
        [HttpGet]
        public ActionResult CreateStatus(string Name, bool Enabled)
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colStatuses = database.GetCollection<Status>(configuration.statusescollection);

            var status = new Status
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Enabled = Enabled
            };

            colStatuses.InsertOne(status);

            return Ok(status);
        }

        // GET: api/groups
        [Route("api/groups")]
        [HttpGet]
        public ActionResult GetGroups()
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colGroups = database.GetCollection<Group>(configuration.groupscollection);

            var result = colGroups
                .AsQueryable()
                .Where(g => g.Enabled)
                .ToList();

            return Ok(result);
        }

        // GET: api/groups/create
        [Route("api/groups/create")]
        [HttpGet]
        public ActionResult CreateGroup(string Name, bool Enabled)
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colGroups = database.GetCollection<Group>(configuration.groupscollection);

            var group = new Group
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Enabled = Enabled
            };

            colGroups.InsertOne(group);

            return Ok(group);
        }

        // GET: api/hours
        [Route("api/hours")]
        [HttpGet]
        public ActionResult GetHours()
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colHours = database.GetCollection<Hours>(configuration.hourscollection);

            var result = colHours
                .AsQueryable()
                .Where(g => g.Enabled)
                .ToList();

            return Ok(result);
        }

        // GET: api/hours/create
        [Route("api/hours/create")]
        [HttpGet]
        public ActionResult CreateHours(string Name, bool Enabled)
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colHours = database.GetCollection<Hours>(configuration.hourscollection);

            var hours = new Hours
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Enabled = Enabled
            };

            colHours.InsertOne(hours);

            return Ok(hours);
        }
    }
}

