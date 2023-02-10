using System.ComponentModel.DataAnnotations;

namespace api_simpleioboard.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string Group { get; set; } = "Core";
        public string MondayStatus { get; set; } = "Office";
        public string TuesdayStatus { get; set; } = "Office";
        public string WednesdayStatus { get; set; } = "Office";
        public string ThursdayStatus { get; set; } = "Office";
        public string FridayStatus { get; set; } = "Office";
        public string MondayHours { get; set; } = "9am-5pm";
        public string TuesdayHours { get; set; } = "9am-5pm";
        public string WednesdayHours { get; set; } = "9am-5pm";
        public string ThursdayHours { get; set; } = "9am-5pm";
        public string FridayHours { get; set; } = "9am-5pm";
        public int Enabled { get; set; } = 1;
    }
}
