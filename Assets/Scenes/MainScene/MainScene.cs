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
        view2ViewModel = AppDomain.CurrentDomain.GetAssemblies()
                                .Where(a => a.GetName().Name == "ViewModels")
                                .SelectMany(a => a.GetTypes())
                                .Where(type => type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(ViewModes.ViewModel<,>))
                                .ToDictionary(type => type.BaseType.GetGenericArguments()[0], type => type);

        View.GetViewModelType = (viewType) => view2ViewModel[viewType];
    }

    void Start()
    {
        session = new Session();

        mainView.model = session;
    }

    void Update()
    {
        session.Run();
    }
}