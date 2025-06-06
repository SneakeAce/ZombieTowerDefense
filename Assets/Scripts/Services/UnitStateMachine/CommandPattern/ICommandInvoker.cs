public interface ICommandInvoker
{
    void AddCommand(ICommand command);
    void ExecuteCommand();
}
