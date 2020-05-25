namespace SisHair.FuncionarioContext.Application.Events
{
    //public delegate void CommandEventHandler<T>(T command) where T : ICommand;

    //public abstract class FuncionarioEventHandler<T> : IFuncionarioEventHandler<T> where T : ICommand
    //{
    //    private event CommandEventHandler<T> Events;

    //    public void InvokeEvents(T command)
    //    {
    //        Events?.Invoke(command);
    //    }

    //    public void SubscribeEvents(Action<T> action)
    //    {
    //        Events += action.Invoke;
    //    }
    //}

    //public delegate void CommandEventHandler(ICommand command);

    //public sealed class FuncionarioEventHandler : IFuncionarioEventHandler
    //{
    //    private event CommandEventHandler Events;

    //    public FuncionarioEventHandler()
    //    {
    //        Events += OnSendEmail;
    //        Events += OnLogRegister;
    //    }

    //    public void SubscribeEvents(Action<CommandEventHandler> action)
    //    {
    //        action?.Invoke(Events);
    //    }

    //    private void OnSendEmail(ICommand sender)
    //    {
    //        Console.WriteLine($"--- Sending Email---\n" +
    //                          $"\tObj: {sender}\n" +
    //                          "\tEvent: {e}");
    //    }

    //    private void OnLogRegister(ICommand sender)
    //    {
    //        Console.WriteLine($"--- Log Register ---\n" +
    //              $"\tObj: {sender}\n" +
    //              "\tEvent: {e}");
    //    }

    //    public void InvokeEvents(ICommand command) =>
    //        Events?.Invoke(command);
    //}
}
