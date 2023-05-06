using CommandTerminal;
using System;

public class Commands
{
    public static Func<Session> GetSession;

    [RegisterCommand(Help = "AddTask", MinArgCount = 1)]
    static void AddTask(CommandArg[] args)
    {
        GetSession().PublishMessage(new MESSAGE_ADD_TASK(new TaskDef() { energy = args[0].Int }));
    }
}
