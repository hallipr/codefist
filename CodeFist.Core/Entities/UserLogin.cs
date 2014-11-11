namespace CodeFist.Core.Entities
{
    using System;

    public class UserLogin
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }

        public String UserId { get; set; }
        public virtual User User { get; set; }
    }
}
