

using System;

namespace n5.webApi.Models.dto
{
    public class EventDto
    {
        public const string MODIFY = "modify";
        public const string REQUEST = "request";
        public const string GET = "get";
        public Guid Id {get; set;}
        public string NameOperation {get; set;}
    }
}