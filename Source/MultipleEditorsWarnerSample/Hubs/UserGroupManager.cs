using System;
using System.Collections.Generic;
using System.Linq;

namespace MultipleEditorsWarnerSample.Hubs
{
    public class UserGroupManager
    {
        private Dictionary<string, Group> Groups = new Dictionary<string, Group>();

        public Group AddUser(string groupName, string userId, string userName)
        {
            lock (this)
            {
                if (!Groups.TryGetValue(groupName, out Group? group))
                {
                    group = new Group(groupName);

                    Groups.Add(groupName, group);
                }

                group.Users.Add(userId, userName);
                group.UserChangeCount++;

                return new Group(group);
            }
        }

        public Group? RemoveUser(string userId)
        {
            lock (this)
            {
                foreach (var keyValue in Groups)
                {
                    Group group = keyValue.Value;

                    if (group.Users.ContainsKey(userId))
                    {
                        group.Users.Remove(userId);
                        group.UserChangeCount++;

                        if (group.Users.Count <= 0)
                        {
                            Groups.Remove(keyValue.Key);

                            return null;
                        }

                        return new Group(group);
                    }
                }

                return null;
            }
        }

        public int AddMessage(string groupId, string userName, DateTime timestamp, string message)
        {
            lock (this)
            {
                if (Groups.TryGetValue(groupId, out Group? group))
                {
                    group.Messages.Add(new GroupMessage(userName, message, timestamp));

                    return group.Messages.Count;
                }

                return 0;
            }
        }
    }
}
