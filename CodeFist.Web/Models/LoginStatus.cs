namespace CodeFist.Web.Models
{
    public class LoginStatus
    {
        public bool Success { get; set; }

        public string UserId { get; set; }

        public string UserDisplayName { get; set; }

        public bool Enabled { get; set; }

        public string Exception { get; set; }
    }
}