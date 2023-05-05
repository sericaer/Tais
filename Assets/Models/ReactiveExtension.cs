using DynamicData;
using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;

public static class ReactiveExtension
{
    public static IObservable<int> Sum<TObject>(this IObservableList<TObject> source, Expression<Func<TObject, int>> propertyAccessor)
        where TObject : INotifyPropertyChanged
    {
        var func = propertyAccessor.Compile();

        return Observable.Merge(
            source.Connect().WhenPropertyChanged(propertyAccessor).Select(_ => source.Items.Sum(func)),
            source.Connect().OnItemRemoved(_ => { }).Select(_ => source.Items.Sum(func)));
    }

    public static IObservable<double> Sum<TObject>(this IObservableList<TObject> source, Expression<Func<TObject, double>> propertyAccessor)
where TObject : INotifyPropertyChanged
    {
        var func = propertyAccessor.Compile();

        return Observable.Merge(
            source.Connect().WhenPropertyChanged(propertyAccessor).Select(_ => source.Items.Sum(func)),
            source.Connect().OnItemRemoved(_ => { }).Select(_ => source.Items.Sum(func)));
    }

    public static IObservable<decimal> Sum<TObject>(this IObservableList<TObject> source, Expression<Func<TObject, decimal>> propertyAccessor)
where TObject : INotifyPropertyChanged
    {
        var func = propertyAccessor.Compile();

        return Observable.Merge(
            source.Connect().WhenPropertyChanged(propertyAccessor).Select(_ => source.Items.Sum(func)),
            source.Connect().OnItemRemoved(_ => { }).Select(_ => source.Items.Sum(func)));
    }
}
