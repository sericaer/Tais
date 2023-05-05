using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;

public class ModelObject : IDisposable, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected MessageBus messageBus;
    private CompositeDisposable disposables = new CompositeDisposable();

    private ModelObject parent;
    private List<ModelObject> children;

    public ModelObject()
    {
        messageBus = new MessageBus();
        messageBus.Regist(this);
    }

    public ModelObject(ModelObject parent)
    {
        messageBus = parent.messageBus;
        messageBus.Regist(this);

        this.parent = parent;
        if(parent.children == null)
        {
            parent.children = new List<ModelObject>();
        }

        parent.children.Add(this);
    }

    public void Dispose()
    {
        disposables.Dispose();
        messageBus.UnRegist(this);

        parent.children.Remove(this);
        parent = null;

        if (children != null)
        {
            foreach(var child in children)
            {
                child.Dispose();
            }

            children.Clear();
        }
    }

    protected void Subscribe<T>(IObservable<T> observable, Action<T> p)
    {
        disposables.Add(observable.Subscribe(p));
    }
} 