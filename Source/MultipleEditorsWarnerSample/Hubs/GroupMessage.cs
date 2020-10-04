using System;

namespace MultipleEditorsWarnerSample.Hubs
{
    public class GroupMessage
    {
        public GroupMessage(string userName, string message, DateTime timeStamp)
        {
            UserName = userName;
            Message = message;
            TimeStamp = timeStamp;
        }        
        public DateTime TimeStamp { get; }

        public string UserName { get; }

        public string Message { get; }
    }
}
