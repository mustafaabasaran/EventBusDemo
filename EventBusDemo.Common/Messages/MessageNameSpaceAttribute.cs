using System;

namespace EventBusDemo.Common.Messages
{
    public class MessageNameSpaceAttribute : Attribute
    {
        public string Namespace { get; set; }

        public MessageNameSpaceAttribute(string _namespace)
        {
            Namespace = _namespace?.ToLowerInvariant();
        }
    }
}