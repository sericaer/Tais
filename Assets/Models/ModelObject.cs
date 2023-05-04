using System;
using System.ComponentModel;
using System.Reactive.Disposables;

public class ModelObject : IDisposable, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected MessageBus messageBus;
    private CompositeDisposable disposables = new CompositeDisposable();

    public ModelObject()
    {
        messageBus = new MessageBus(this);
    }

    public void Dispose()
    {
        disposables.Dispose();
        messageBus.Dispose();
    }

    protected void Subcribe<T>(IObservable<T> observable, Action<T> p)
    {
        disposables.Add(observable.Subscribe(p));
    }
}