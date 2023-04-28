using System;
using System.Linq.Expressions;
using UnityEngine.EventSystems;
using UnityEngine;

namespace ViewModes
{
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
}
