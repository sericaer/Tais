
namespace ViewModes
{
    public class ProvinceItemViewModel : ViewModel<ProvinceItemView, Province>
    {
        public override void OnBindContext(ProvinceItemView view, Province model)
        {
            view.provName.BindTo(model, model => model.name).RegistTo(this);
            view.popNum.BindTo(model.pops.Sum(x=>x.popNum)).RegistTo(this);
        }
    }
}
