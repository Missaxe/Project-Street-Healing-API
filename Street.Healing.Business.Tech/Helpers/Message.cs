using MimeKit;

namespace Street.Healing.Business.Tech.Helpers
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsHtml { get; set; }
        public Message(IEnumerable<string> to, string subject, string content, bool ishtml)
        {
            To = [.. to.Select(x => new MailboxAddress("email", x))];
            Subject = subject;
            Content = content;
            IsHtml = ishtml;
        }
    }
}
