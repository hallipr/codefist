namespace CodeFist.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public class AccessRequest
    {
        public Dictionary<string, object> Properties { get; set; }

        public string Type { get; set; }

        public T Property<T>(string name)
        {
            object value;
            if (!this.Properties.TryGetValue(name, out value))
            {
                return default(T);
            }

            return (T) value;
        }

        public static AccessRequest Create(string accessType, object properties)
        {
            return new AccessRequest
            {
                Type = accessType,
                Properties = CreateDictionary(properties)
            };
        }

        private static Dictionary<string, object> CreateDictionary(object value)
        {
            if (value == null)
            {
                return new Dictionary<string, object>();
            }

            var dictionary = value as Dictionary<string, object>;
            if (dictionary != null)
            {
                return dictionary;
            }

            return TypeDescriptor.GetProperties(value).OfType<PropertyDescriptor>()
                .ToDictionary(p => p.Name, p => p.GetValue(value));
        }
    }
}