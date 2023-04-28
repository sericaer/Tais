namespace ViewModes
{
    public class MainViewModel : ViewModel<MainView, Session>
    {
        public override void OnBindContext(MainView view, Session model)
        {
            view.popNum.BindOneWay(x => x.text).From(model, model => model.popNum, (num) => num.ToString()).RegistTo(this);
        }
    }
}
