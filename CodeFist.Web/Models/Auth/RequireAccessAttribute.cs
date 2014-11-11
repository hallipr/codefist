namespace CodeFist.Web.Models.Auth
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class RequireAccessAttribute : Attribute
    {
        public string AccessType { get; private set; }
        public PropertyMap[] Parameters { get; private set; }

        private static readonly Regex PropertyRegex = new Regex(@"\s*(?<argumentName>\w+)\s*(=>\s*(?<propertyName>\w+)\s*)?");

        public RequireAccessAttribute(string accessType, params string[] parameters)
        {
            this.AccessType = accessType;
            this.Parameters = parameters.Select(ParseParameter).ToArray();
        }

        private static PropertyMap ParseParameter(string arg)
        {
            var match = PropertyRegex.Match(arg);
            var argumentName = match.Groups["argumentName"].Value;
            var propertyNameGroup = match.Groups["propertyName"];
            var propertyName = propertyNameGroup.Success ? propertyNameGroup.Value : argumentName;

            return new PropertyMap
            {
                ArgumentName = argumentName, 
                PropertyName = propertyName
            };
        }
    }
}