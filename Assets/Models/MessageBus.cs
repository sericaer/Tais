using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

internal class OnMessageAttribute : Attribute
{
}

