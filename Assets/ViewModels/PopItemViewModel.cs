
namespace ViewModes
{
    public class PopItemViewModel : ViewModel<PopItemView, Pop>
    {
        public override void OnBindContext(PopItemView view, Pop model)
        {
            //view.provName.BindTo(model, model => model.name).RegistTo(this);
            view.popNum.BindTo(model, model=>model.popNum).RegistTo(this);
        }
    }
}
