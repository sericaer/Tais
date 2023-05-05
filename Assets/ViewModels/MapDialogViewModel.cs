namespace ViewModes
{
    public class MapDialogViewModel : ViewModel<MapDialogView, Session>
    {
        public override void OnBindContext(MapDialogView view, Session model)
        {
            view.provinceItemView.BindCollection(model.provinces).RegistTo(this);
        }
    }
}
