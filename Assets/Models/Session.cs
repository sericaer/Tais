using System;
using System.ComponentModel;

public class Session : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public int popNum { get; set; }

    public ICommand nextTurnCommand { get; }


    public Session()
    {
        nextTurnCommand = new NextTurnCommand();
    }
}


public interface ICommand : INotifyPropertyChanged
{
    bool isEnable { get; }
    void Exec();
}

public class NextTurnCommand : ICommand
{
    public event PropertyChangedEventHandler PropertyChanged;

    public bool isEnable { get; set; } = true;

    public void Exec()
    {
        isEnable = false;
    }
}