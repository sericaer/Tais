namespace ViewModes
{
    public class ProvinceDialogViewModel : ViewModel<ProvinceDialogView, Province>
    {
        public override void OnBindContext(ProvinceDialogView view, Province model)
        {
            view.popItemView.BindCollection(model.pops).RegistTo(this);
        }
    }
}
