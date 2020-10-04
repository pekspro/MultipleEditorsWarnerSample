using System.Collections.Generic;

namespace MultipleEditorsWarnerSample.Hubs
{
    public class Group
    {
        public Group(string groupId)
        {
            GroupName = groupId;

            Messages = new List<GroupMessage>();

            Users = new Dictionary<string, string>();
        }

        public Group(Group group)
        {
            Messages = new List<GroupMessage>(group.Messages);

            Users = new Dictionary<string, string>(group.Users);

            GroupName = group.GroupName;
            UserChangeCount = group.UserChangeCount;
        }

        public string GroupName { get; set; }

        public List<GroupMessage> Messages { get; set; }

        public Dictionary<string, string> Users { get; set; }

        public int UserChangeCount { get; set; }
    }
}
