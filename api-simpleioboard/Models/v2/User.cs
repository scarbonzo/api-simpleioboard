namespace api_simpleioboard.Models.v2
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string Group { get; set; } = "Core";
        public int Year { get; set; } = DateTime.Now.Year;
        public int Week { get; set; } = 0;
        public string MondayStatus { get; set; } = "Office";
        public string TuesdayStatus { get; set; } = "Office";
        public string WednesdayStatus { get; set; } = "Office";
        public string ThursdayStatus { get; set; } = "Office";
        public string FridayStatus { get; set; } = "Office";
        public string MondayStart { get; set; } = "9am";
        public string TuesdayStart { get; set; } = "9am";
        public string WednesdayStart { get; set; } = "9am";
        public string ThursdayStart { get; set; } = "9am";
        public string FridayStart { get; set; } = "9am";
        public string MondayEnd { get; set; } = "5pm";
        public string TuesdayEnd { get; set; } = "5pm";
        public string WednesdayEnd { get; set; } = "5pm";
        public string ThursdayEnd { get; set; } = "5pm";
        public string FridayEnd { get; set; } = "5pm";
        public string MondayNotes { get; set; } = "";
        public string TuesdayNotes { get; set; } = "";
        public string WednesdayNotes { get; set; } = "";
        public string ThursdayNotes { get; set; } = "";
        public string FridayNotes { get; set; } = "";
        public string WeekNotes { get; set; } = "";
        public int Enabled { get; set; } = 1;
    }
}
