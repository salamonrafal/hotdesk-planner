namespace Core.Models
{
    public class Localization
    {
        public int Id { get; set; }
        public int Floor { get; set; }
        public string Outbuilding { get; set; }
        public Coordination Coordination { get; set; }
    }
}
