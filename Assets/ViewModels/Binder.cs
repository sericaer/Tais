using System;
using System.Linq.Expressions;
using System.ComponentModel;
using ReactiveMarbles.PropertyChanged;

namespace ViewModes
{
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
