using DynamicData.Binding;
using ReactiveMarbles.PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reflection;

public class Session : ModelObject, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public bool isRunning { get; set; } = true;

    public int popNum { get; set; }

    public Date date { get; } = new Date();

    public Session()
    {
        Subcribe(date.WhenValueChanged(x => x.month), _ => messageBus.Publish(new MESSAGE_MONTH_INC(date.year, date.month)));
    }

    public void Run()
    {
        if(isRunning)
        {
            date.DaysInc();
        }
    }

    [OnMessage]
    public void OnMESSAGE_MONTH_INC(MESSAGE_MONTH_INC msg)
    {
        isRunning = false;
    }
}

internal class OnMessageAttribute : Attribute
{
}

public class MESSAGE_MONTH_INC
{
    public MESSAGE_MONTH_INC(int year, int month)
    {
        Year = year;
        Month = month;
    }

    public int Year { get; }
    public int Month { get; }
}

public class MessageBus : IDisposable
{
    private Dictionary<Type, List<(object, MethodInfo)>> dictionary = new Dictionary<Type, List<(object, MethodInfo)>>();

    public MessageBus(object session)
    {
        var methods = session.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        foreach(var method in methods.Where(m=>m.GetCustomAttribute<OnMessageAttribute>() != null))
        {
            var parameters = method.GetParameters();
            if(parameters.Length != 1)
            {
                throw new Exception();
            }

            var msgType = parameters[0].ParameterType;
            if (!dictionary.ContainsKey(msgType))
            {
                dictionary.Add(msgType, new List<(object, MethodInfo)>());
            }

            dictionary[msgType].Add((session, method));
        }
    }

    public void Dispose()
    {
        dictionary.Clear();
    }

    internal void Publish<T>(T message)
    {
        var parameters = new object[] { message };
        if(dictionary.TryGetValue(message.GetType(), out List<(object obj, MethodInfo method)> list))
        {
            foreach(var item in list)
            {
                item.method.Invoke(item.obj, parameters);
            }
        }
    }
}

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

public class ModelObject : IDisposable
{
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