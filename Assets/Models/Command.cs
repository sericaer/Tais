using System;
using System.ComponentModel;

public interface ICommand : INotifyPropertyChanged
{
    bool isEnable { get; set; }
    void Exec();
}

public class Command : ICommand
{
    public event PropertyChangedEventHandler PropertyChanged;

    public bool isEnable { get; set ; }

    public Action action;

    public void Exec()
    {
        action.Invoke();
    }
}
