using DynamicData.Aggregation;

namespace ViewModes
{
    public class MainViewModel : ViewModel<MainView, Session>
    {
        public override void OnBindContext(MainView view, Session model)
        {
            view.popNum.BindTo(model.provinces.Sum(x=>x.popNum)).RegistTo(this);

            view.day.BindTo(model.date, date => date.day).RegistTo(this);
            view.month.BindTo(model.date, date => date.month).RegistTo(this);
            view.year.BindTo(model.date, date => date.year).RegistTo(this);

            view.playerEnergy.BindToOneWay(model.player, player => player.energy.currValue).RegistTo(this);

            view.taskItemView.BindCollection(model.player.tasks).RegistTo(this);

            var nextTurnCommand = new Command()
            {
                action = () =>
                {
                    model.isRunning = true;
                }
            };

            nextTurnCommand.BindOneWay(x => x.isEnable).From(model, model => model.isRunning, flag => !flag).RegistTo(this);
            view.nextTurnButton.BindCommand().From(nextTurnCommand).RegistTo(this);
        }
    }
}
