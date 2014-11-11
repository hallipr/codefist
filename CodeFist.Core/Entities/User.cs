namespace CodeFist.Core.Entities
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }

        public bool Enabled { get; set; }

        public virtual ICollection<UserLogin> Logins { get; set; }
        public virtual ICollection<Bot> Bots { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}