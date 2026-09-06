namespace GoofyAhh.Common.Commands;

public interface ICommand
{
    string Name { get; }
    void Execute();
}