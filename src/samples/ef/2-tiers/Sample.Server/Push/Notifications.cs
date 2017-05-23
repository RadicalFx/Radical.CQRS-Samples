using System;

namespace Sample.Server.Push
{
    public class SuccessNotification
    {
        public object Result { get; set; }
        public string CorrelationId { get; set; }
    }

    public class FailureNotification
    {
        public Exception Error { get; set; }
        public string CorrelationId { get; set; }
    }
}
