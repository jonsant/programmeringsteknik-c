using System;

namespace Users.Common.Models
{
    public class ServiceResponse : IServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
