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

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        if (_model == null)
        {
            Debug.LogWarning($"{name} model is null");
            return;
        }

        if(GetViewModelType == null)
        {
            Debug.LogWarning($"GetViewModelType is null");
            return;
        }

        var viewModelType = GetViewModelType(GetType());
        if (viewModelType == null)
        {
            Debug.LogWarning($"GetViewModelType({GetType()}) is null");
            return;
        }

        viewModel = Activator.CreateInstance(viewModelType) as IViewModel;
        viewModel.BindContext(this, _model);
    }

    void OnDestroy()
    {
        viewModel?.Dispose();
    }
}
