using System;
using UnityEngine;
using ViewModes;

public abstract class View : MonoBehaviour, IView
{
    public static Func<Type, Type> GetViewModelType;

    public object model
    {
        get
        {
            return _model;
        }
        set
        {
            _model = value;
        }
    }

    private object _model;
    private IViewModel viewModel;

    void Start()
    {
        if (_model == null)
        {
            Debug.Log($"{name} model is null");
            return;
        }

        viewModel = Activator.CreateInstance(GetViewModelType(GetType())) as IViewModel;
        viewModel.BindContext(this, _model);
    }

    void OnDestroy()
    {
        viewModel?.Dispose();
    }
}
