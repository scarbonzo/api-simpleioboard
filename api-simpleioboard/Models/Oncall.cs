using MongoDB.Driver.Linq;
using System.Globalization;

namespace api_simpleioboard.Models
{
    public class Oncall
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Year { get; set; } = 0;
        public int Week { get; set; } = 0;
        public string Team { get; set; } = "";
        public string Primary { get; set; } = "";
        public string Backup { get; set; } = "";
    }
}
