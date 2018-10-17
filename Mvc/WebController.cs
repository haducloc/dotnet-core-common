using NetCore.Common.Services;
using System;

namespace NetCore.Common.Mvc
{
    public abstract class WebController : MvcController
    {
        public const string ParamAction = "formAction";

        public const string ActionSave = "save";
        public const string ActionSaveAndNew = "saveAndNew";
        public const string ActionDelete = "delete";
        public const string ActionDeleteAndNew = "deleteAndNew";

        public WebController(Config config) : base(config)
        {
        }

        public bool IsActionSave()
        {
            string action = Request.Form[ParamAction];
            return ActionSave.Equals(action, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsActionSaveAndNew()
        {
            string action = Request.Form[ParamAction];
            return ActionSaveAndNew.Equals(action, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsActionDelete()
        {
            string action = Request.Form[ParamAction];
            return ActionDelete.Equals(action, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsActionDeleteAndNew()
        {
            string action = Request.Form[ParamAction];
            return ActionDeleteAndNew.Equals(action, StringComparison.OrdinalIgnoreCase);
        }

        public void AddInfo(string message) => AddMessage(new Message(Message.Info, message));

        public void AddNotice(string message) => AddMessage(new Message(Message.Notice, message));

        public void AddWarning(string message) => AddMessage(new Message(Message.Warning, message));

        public void AddError(string message) => AddMessage(new Message(Message.Error, message));

        private void AddMessage(Message message)
        {
            Messages messages = TempData.Get<Messages>(Messages.InstanceID);
            if (messages == null)
            {
                messages = new Messages();
            }
            messages.Add(message);
            TempData.Put<Messages>(Messages.InstanceID, messages);
        }
    }
}
