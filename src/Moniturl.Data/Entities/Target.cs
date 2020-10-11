namespace Moniturl.Data
{
    public class Target : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Interval { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }

    }
}
