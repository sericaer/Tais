using System.Reactive.Disposables;
using System;

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
}
