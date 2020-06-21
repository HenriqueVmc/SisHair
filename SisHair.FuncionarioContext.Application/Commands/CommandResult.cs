using SisHair.CoreContext.BaseInterfaces;
using System.Collections.Generic;

namespace SisHair.FuncionarioContext.Application.Commands
{
    public class CommandResult : ICommandResult
    {
        private readonly List<string> notifications;

        public CommandResult AddNotifications(string message)
        {
            notifications.Add(message);
            return this;
        }

        public List<string> GetNotifications() =>
            notifications;

        public CommandResult() { }

        public CommandResult(bool state, string message)
        {
            this.State = state;
            this.Message = message;
            this.notifications = new List<string>();
        }

        public bool State { get; private set; }
        public string Message { get; private set; }
    }
}
