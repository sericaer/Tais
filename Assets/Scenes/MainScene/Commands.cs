using CommandTerminal;

public class Commands
{
    [RegisterCommand(Help = "Clear the command console", MaxArgCount = 0)]
    static void CommandClear1(CommandArg[] args)
    {
        Terminal.Buffer.Clear();
    }
}
