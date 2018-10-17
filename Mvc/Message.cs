using System;

namespace NetCore.Common.Mvc
{

    [Serializable]
    public class Message
    {
        public const int Info = 1;
        public const int Notice = 2;
        public const int Warning = 3;
        public const int Error = 4;
        
        public int Type { get; set; }

        public string Text { get; set; }

        public Message()
        {
        }

        public Message(int type, string text)
        {
            this.Type = type;
            this.Text = text;
        }

        public bool IsInfo => this.Type == Info;

        public bool IsNotice => this.Type == Notice;

        public bool IsWarning => this.Type == Warning;

        public bool IsError => this.Type == Error;
    }
}
