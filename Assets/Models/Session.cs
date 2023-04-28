using System.ComponentModel;

public class Session : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;


    public int popNum { get; set; }
}
