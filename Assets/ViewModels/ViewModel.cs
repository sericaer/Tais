using System.Reactive.Disposables;
using System;
using System.Linq.Expressions;
using UnityEngine.EventSystems;
using System.ComponentModel;
using UnityEngine;
using ReactiveMarbles.PropertyChanged;

namespace ViewModes
{
    public abstract class ViewModel<TView, TModel> : IViewModel
    {
        private CompositeDisposable disposables = new CompositeDisposable();

        public void AddSubcrible(IDisposable disposable)
        {
            disposables.Add(disposable);
        }

        public void Dispose()
        {
            disposables?.Dispose();
        }

        public abstract void OnBindContext(TView view, TModel model);

        public void BindContext(object view, object model)
        {
            OnBindContext((TView)view, (TModel)model);
        }
    }

    public static class Extensions
    {
        public static Binder<TTarget, TProperty> BindOneWay<TTarget, TProperty>(this TTarget ui, Expression<Func<TTarget, TProperty>> func)
            where TTarget : UIBehaviour
        {
            return new Binder<TTarget, TProperty>(ui, func);
        }

        public static void RegistTo(this IDisposable disposable, IViewModel view)
        {
            Debug.Log($"add dispose {disposable.GetHashCode()} to {view.GetHashCode()}");

            view.AddSubcrible(disposable);
        }
    }

    public class Binder<TTarget, TProperty>
    {
        private TTarget target;
        private Expression<Func<TTarget, TProperty>> targetProperty;

        public Binder(TTarget target, Expression<Func<TTarget, TProperty>> targetProperty)
        {
            this.target = target;
            this.targetProperty = targetProperty;
        }

        public IDisposable From<TFrom>(TFrom from, Expression<Func<TFrom, TProperty>> fromProperty)
            where TFrom : class, INotifyPropertyChanged
        {
            return from.BindOneWay(target, fromProperty, targetProperty);
        }

        public IDisposable From<TFrom, TFromProperty>(TFrom from, Expression<Func<TFrom, TFromProperty>> fromProperty, Func<TFromProperty, TProperty> conversionFunc)
            where TFrom : class, INotifyPropertyChanged
        {
            var disposable = from.BindOneWay(target, fromProperty, targetProperty, conversionFunc);
            return disposable;
        }


        public IDisposable From<TFromProperty>(IObservable<TFromProperty> observable, Func<TFromProperty, TProperty> conversionFunc)
        {
            var disposable = observable.Subscribe(x => conversionFunc(x));
            return disposable;
        }
    }
}
