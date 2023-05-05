using System;
using System.Linq.Expressions;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using DynamicData.Binding;
using UnityEngine.Events;
using ReactiveMarbles.PropertyChanged;
using System.ComponentModel;
using DynamicData;
using System.Reactive.Linq;
using System.Linq;

namespace ViewModes
{
    public static class Extensions
    {
        public static Binder<TTarget, TProperty> BindOneWay<TTarget, TProperty>(this TTarget ui, Expression<Func<TTarget, TProperty>> func)
        {
            return new Binder<TTarget, TProperty>(ui, func);
        }

        public static void RegistTo(this IDisposable disposable, IViewModel view)
        {
            Debug.Log($"add dispose {disposable.GetHashCode()} to {view.GetHashCode()}");

            view.AddSubcrible(disposable);
        }

        public static BinderCommand BindCommand(this Button button)
        {
            return new BinderCommand(button);
        }

        public static BinderText1<TFrom, TFromProperty> BindTo<TFrom, TFromProperty>(this Text text, TFrom from, Expression<Func<TFrom, TFromProperty>> fromProperty)
                        where TFrom : class, INotifyPropertyChanged
        {
            return new BinderText1<TFrom, TFromProperty>(text, from, fromProperty);
        }

        public static BinderText0<T> BindTo<T>(this Text text, IObservable<T> observable)
        {
            return new BinderText0<T>(text, observable);
        }

        public static BinderCollection<TItemView, TItemModel> BindCollection<TItemView, TItemModel>(this TItemView itemView, IObservableList<TItemModel> observableList)
    where TItemView : CollectionItemView
        {
            return new BinderCollection<TItemView, TItemModel>(itemView, observableList);
        }
    }

    public class BinderCollection<TItemView, TItemModel>
        where TItemView : CollectionItemView
    {
        private TItemView itemView;
        private IObservableList<TItemModel> observableList;

        public BinderCollection(TItemView itemView, IObservableList<TItemModel> observableList)
        {
            itemView.gameObject.SetActive(false);

            this.itemView = itemView;
            this.observableList = observableList;
        }

        public void RegistTo(IViewModel viewModel)
        {
            var disposeAdd = observableList.Connect().OnItemAdded(item => 
            {
                var newView = itemView.Clone();
                newView.model = item;
            }).Subscribe();

            var disposeRemove = observableList.Connect().OnItemRemoved(item => 
            {
                var oldView = itemView.transform.parent.GetComponentsInChildren<TItemView>().SingleOrDefault(x => x.model == (object)item);
                oldView?.Destroy();
            }).Subscribe();

            viewModel.AddSubcrible(disposeAdd);
            viewModel.AddSubcrible(disposeRemove);
        }
    }

    public class BinderText0<T>
    {
        private Text text;
        private IObservable<T> observable;

        public BinderText0(Text text, IObservable<T> observable)
        {
            this.text = text;
            this.observable = observable;
        }

        public void RegistTo(IViewModel viewModel)
        {
            var dispose = observable.Subscribe(o => text.text = o.ToString());
            viewModel.AddSubcrible(dispose);
        }
    }

    public class BinderText1<TFrom, TFromProperty> where TFrom : class, INotifyPropertyChanged
    {
        private Text text;
        private TFrom from;
        private Expression<Func<TFrom, TFromProperty>> fromProperty;

        public BinderText1(Text text, TFrom from, Expression<Func<TFrom, TFromProperty>> fromProperty)
        {
            this.text = text;
            this.from = from;
            this.fromProperty = fromProperty;
        }

        public void RegistTo(IViewModel viewModel)
        {
            var dispose = from.BindOneWay(text, fromProperty, text=>text.text, property=> property.ToString());
            viewModel.AddSubcrible(dispose);
        }
    }

    public class BinderCommand
    {
        private Button button;

        public BinderCommand(Button button)
        {
            this.button = button;
        }

        internal IDisposable From(ICommand nextTurnCommand)
        {
            return new BinderCommandRslt(button, nextTurnCommand);

        }

        class BinderCommandRslt : IDisposable
        {
            private Action Disposable;

            public BinderCommandRslt(Button button, ICommand nextTurnCommand)
            {
                var interactableSubscribe = button.BindOneWay(x => x.interactable).From(nextTurnCommand, cmd => cmd.isEnable);
                
                UnityAction listener = () => nextTurnCommand.Exec();
                button.onClick.AddListener(listener);

                Disposable = () =>
                {
                    interactableSubscribe.Dispose();
                    button.onClick.RemoveListener(listener);
                };
            }

            public void Dispose()
            {
                Disposable.Invoke();
            }
        }
    }
}
