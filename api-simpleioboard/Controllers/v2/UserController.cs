using api_simpleioboard.Models.v2;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Globalization;

namespace api_simpleioboard.Controllers.v2
{
    public class UserController : Controller
    {
        // GET: UserController
        [Route("api/v2/user")]
        [HttpGet]
        public ActionResult GetAll()
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colUsers = database.GetCollection<User>(configuration.usersv2collection);

            var result = colUsers
                .AsQueryable()
                .Where(u => u.Enabled == 1)
                .ToList();

            return Ok(result);
        }

        // GET: UserController
        [Route("api/v2/users")]
        [HttpGet]
        public ActionResult GetUsers(int? year, int? week, string? group)
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colUsers = database.GetCollection<User>(configuration.usersv2collection);

            if (year == null)
            {
                year = DateTime.Now.Year;
            }

            if (week == null)
            {
                week = CurrentWeekNumber();
            }

            var result = colUsers
                .AsQueryable()
                .Where(u => u.Enabled == 1)
                .Where(u => u.Year == year)
                .Where(u => u.Week == week);

            if(group != null)
            {
                result = result.Where(u => u.Group == group);
            }

            return Ok(result.ToList());
        }

        // GET: api/user/reodice
        [Route("api/v2/users/{username}")]
        [HttpGet]
        public ActionResult Details(string username, bool excludepast = true)
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colUsers = database.GetCollection<User>(configuration.usersv2collection);

            var week = CurrentWeekNumber();

            var result = colUsers
                .AsQueryable()
                .Where(u => u.Username == username);

            if(excludepast)
            {
                result = result.Where(u => u.Week >= week);
            }

            return Ok(result.OrderBy(u => u.Week).ToList());
        }

        // GET: api/group/Core
        [Route("api/v2/group/{group}")]
        [HttpGet]
        public ActionResult Group(string group)
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colUsers = database.GetCollection<User>(configuration.usersv2collection);

            var result = colUsers
                .AsQueryable()
                .Where(u => u.Group == group)
                .ToList();

            return Ok(result);
        }

        // GET: api/user/create
        [Route("api/v2/user/create")]
        [HttpGet]
        public ActionResult Create(
        string username = "reodice",
        string firstName = "Rich",
        string group = "Core",
        int year = 2023,
        int week = 6,
        string mondayStatus = "WFH",
        string tuesdayStatus = "WFH",
        string wednesdayStatus = "Office",
        string thursdayStatus = "WFH",
        string fridayStatus = "WFH",
        string mondayStart = "8am",
        string tuesdayStart = "8am",
        string wednesdayStart = "8am",
        string thursdayStart = "8am",
        string fridayStart = "8am",
        string mondayEnd = "4pm",
        string tuesdayEnd = "4pm",
        string wednesdayEnd = "4pm",
        string thursdayEnd = "4pm",
        string fridayEnd = "4pm",
        string mondayNotes = "",
        string tuesdayNotes = "",
        string wednesdayNotes = "",
        string thursdayNotes = "",
        string fridayNotes = "",
        string weekNotes = "",
        int enabled = 1)
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colUsers = database.GetCollection<User>(configuration.usersv2collection);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                FirstName = firstName,
                Group = group,
                Year = year,
                Week = week,
                MondayStatus = mondayStatus,
                MondayStart = mondayStart,
                MondayEnd = mondayEnd,
                MondayNotes = mondayNotes,
                TuesdayStatus = tuesdayStatus,
                TuesdayStart = tuesdayStart,
                TuesdayEnd = tuesdayEnd,
                TuesdayNotes = tuesdayNotes,
                WednesdayStatus = wednesdayStatus,
                WednesdayStart = wednesdayStart,
                WednesdayEnd = wednesdayEnd,
                WednesdayNotes = wednesdayNotes,
                ThursdayStatus = thursdayStatus,
                ThursdayStart = thursdayStart,
                ThursdayEnd = thursdayEnd,
                ThursdayNotes = thursdayNotes,
                FridayStatus = fridayStatus,
                FridayStart = fridayStart,
                FridayEnd = fridayEnd,
                FridayNotes = fridayNotes,
                WeekNotes = weekNotes,
                Enabled = enabled
            };

            colUsers.InsertOne(user);

            return Ok(user);
        }

        /*
        // GET: api/user/update
        [Route("api/v2/user/update")]
        [HttpGet]
        public ActionResult Update(string username, string firstname, string group,
            string mondayStatus, string tuesdayStatus, string wednesdayStatus, string thursdayStatus, string fridayStatus,
            string mondayHours, string tuesdayHours, string wednesdayHours, string thursdayHours, string fridayHours,
            int? enabled)
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colUsers = database.GetCollection<User>(configuration.usersv2collection);

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
        */

        // GET: api/user/reodice
        [Route("api/v2/user/delete/{username}")]
        [HttpGet]
        public ActionResult Delete(string username)
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colUsers = database.GetCollection<User>(configuration.usersv2collection);

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

        static public int CurrentWeekNumber()
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }
    }
}

