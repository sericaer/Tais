using System;
using System.Linq.Expressions;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using DynamicData.Binding;
using UnityEngine.Events;

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
