namespace CodeFist.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    internal class JsonNetResult : ActionResult
    {
        public JsonNetResult(object data, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            this.Data = data;
            this.JsonRequestBehavior = behavior;
        }

        public JsonRequestBehavior JsonRequestBehavior { get; set; }

        public object Data { get; set; }

        public int? MaxDepth { get; set; }

        public System.Text.Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; }
 
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("GET not allowed for JSON request");

            var response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;
            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;
            if (this.Data == null)
                return;

            var settings = new JsonSerializerSettings
            {
                MaxDepth = this.MaxDepth,
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            response.Write(JsonConvert.SerializeObject(this.Data, settings));
        }
    }
}