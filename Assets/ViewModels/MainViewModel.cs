namespace ViewModes
{
    public class MainViewModel : ViewModel<MainView, Session>
    {
        public override void OnBindContext(MainView view, Session model)
        {
            view.popNum.BindOneWay(x => x.text).From(model, model => model.popNum, (num) => num.ToString()).RegistTo(this);

            view.day.BindOneWay(x=>x.text).From(model, model => model.date.day, (num) => num.ToString()).RegistTo(this);
            view.month.BindOneWay(x => x.text).From(model, model => model.date.month, (num) => num.ToString()).RegistTo(this);
            view.year.BindOneWay(x => x.text).From(model, model => model.date.year, (num) => num.ToString()).RegistTo(this);

            view.nextTurnButton.BindCommand().From(model.nextTurnCommand).RegistTo(this);
        }
    }
}
