namespace Moniturl.Core
{
    public class TargetLogDto : BaseDto
    {
        public string StatusCode { get; set; }
        public int? TargetId { get; set; }
        public string CreatedDateAsString
        {
            get
            {
                return CreatedDate.ToString("dd.MM.yyyy HH:mm:ss");
            }
        }
    }
}
