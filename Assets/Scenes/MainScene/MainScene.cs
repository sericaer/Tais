using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    public MainView mainView;

    Session session;


    void Awake()
    {
        View.GetViewModelType = (viewType) =>
        {
            Debug.Log(Application.dataPath);


            var types = AppDomain.CurrentDomain.GetAssemblies()
                                .Where(a => a.GetName().Name == "ViewModels")
                                .SelectMany(a => a.GetTypes());

            foreach(var type in types)
            {
                var baseType = type.BaseType;
                if(baseType != null)
                {
                    if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(ViewModes.ViewModel<,>))
                    {
                        var args = baseType.GetGenericArguments();
                        if(args[0] == viewType)
                        {
                            return type;
                        }
                    }
                }
            }


            return null;
        };
    }

    void Start()
    {
        session = new Session();

        mainView.model = session;
    }

    void Update()
    {
        session.popNum++;
    }
}