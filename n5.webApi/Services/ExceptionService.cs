using System;

namespace n5.webApi.Services
{
    public class ExceptionService : Exception
    {
        public ExceptionService(string? message) : base(message) { }
    }
}
