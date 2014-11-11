namespace CodeFist.Web.Models.Auth
{
    using System;
    using Business;

    public class SecurityRule
    {
        public SecurityRule(string claimType, Predicate<AccessRequest> accessCheck)
        {
            this.ClaimType = claimType;
            this.AccessCheck = accessCheck;
        }

        public string ClaimType { get; set; }

        public Predicate<AccessRequest> AccessCheck { get; set; }
    }
}