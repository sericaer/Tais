using CommandTerminal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    public MainView mainView;

    Session session;

    private Dictionary<Type, Type> view2ViewModel;

    void Awake()
    {
        Commands.GetSession = () => session;
        Terminal.Shell.RegisterCommands(typeof(Commands));

        view2ViewModel = AppDomain.CurrentDomain.GetAssemblies()
                                .Where(a => a.GetName().Name == "ViewModels")
                                .SelectMany(a => a.GetTypes())
                                .Where(type => type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(ViewModes.ViewModel<,>))
                                .ToDictionary(type => type.BaseType.GetGenericArguments()[0], type => type);

        View.GetViewModelType = (viewType) =>
        {
            if(view2ViewModel.TryGetValue(viewType, out Type value))
            {
                return value;
            }
            return null;
        };
    }

    void Start()
    {
        session = new Session();

        mainView.model = session;
    }

    void FixedUpdate()
    {
        session.Run();
    }
}