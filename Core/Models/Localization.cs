namespace Core.Models
{
    public class Localization : BaseModel
    {
        public int Floor { get; set; }
        public string Outbuilding { get; set; }
        public Coordination Coordination { get; set; }
    }
}
