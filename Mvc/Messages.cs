using System;
using System.Collections.Generic;

namespace NetCore.Common.Mvc
{

    [Serializable]
    public class Messages : List<Message>
    {
        public const string InstanceID = nameof(Messages);

        public Messages()
        {
        }

        public Messages(int capacity) : base(capacity)
        {
        }

        public void AddInfo(string text) => this.Add(new Message(Message.Info, text));

        public void AddNotice(string text) => this.Add(new Message(Message.Notice, text));

        public void AddWarning(string text) => this.Add(new Message(Message.Warning, text));

        public void AddError(string text) => this.Add(new Message(Message.Error, text));
    }
}
