namespace api_simpleioboard.Models
{
    public class Group
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public bool Enabled { get; set; } = true;
    }
}
