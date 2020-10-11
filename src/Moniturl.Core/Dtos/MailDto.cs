namespace Moniturl.Core
{
    public class MailDto : BaseDto
    {
        public string To { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsSend { get; set; }
    }
}
