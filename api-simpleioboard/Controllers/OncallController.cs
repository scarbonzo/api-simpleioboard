using api_simpleioboard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace api_simpleioboard.Controllers
{
    [ApiController]
    public class OncallController : ControllerBase
    {
        // GET: OncallController
        [Route("api/oncall")]
        [HttpGet]
        public ActionResult GetTeamOncall(int year, int week, string team)
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colOncall = database.GetCollection<Oncall>(configuration.oncallscollection);

            var result = colOncall
                .AsQueryable()
                .Where(o => o.Year == year)
                .Where(o => o.Week == week)
                .Where(o => o.Team == team)
                .ToList();

            return Ok(result);
        }

        // GET: api/oncall/create
        [Route("api/oncall/create")]
        [HttpGet]
        public ActionResult CreateOncall(int year, int week, string team, string primary, string backup)
        {
            var dbClient = new MongoClient(configuration.dbconnection);
            var database = dbClient.GetDatabase(configuration.dbname);
            var colOncall = database.GetCollection<Oncall>(configuration.oncallscollection);

            var status = new Oncall
            {
                Id = Guid.NewGuid(),
                Year = year,
                Week = week,
                Team = team,
                Primary = primary,
                Backup = backup
            };

            colOncall.InsertOne(status);

            return Ok(status);
        }
    }
}
