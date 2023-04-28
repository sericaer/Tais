using System.Reactive.Disposables;
using System;

namespace ViewModes
{
    public interface IViewModel : IDisposable
    {
        void BindContext(object view, object obj);
        void AddSubcrible(IDisposable disposable);
    }
}
