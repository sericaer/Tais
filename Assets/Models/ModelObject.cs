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
        messageBus = new MessageBus();
        messageBus.Regist(this);
    }

    public ModelObject(ModelObject parent)
    {
        messageBus = parent.messageBus;
        messageBus.Regist(this);
    }

    public void Dispose()
    {
        disposables.Dispose();
        messageBus.UnRegist(this);
    }

    protected void Subscribe<T>(IObservable<T> observable, Action<T> p)
    {
        disposables.Add(observable.Subscribe(p));
    }
} 