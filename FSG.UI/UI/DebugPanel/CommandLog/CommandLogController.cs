using FSG.Commands;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class CommandLogController : UIController
    {
        private readonly Grid _commandLog;
        private int _count = 1;

        public CommandLogController(UIServiceProvider uiServiceProvider)
            : base("../../../UI/DebugPanel/CommandLog/CommandLog.xaml", uiServiceProvider)
        {
            _commandLog = (Grid)Root.FindWidgetById("CommandLogGrid");
        }

        public override void Update(ICommand command)
        {
            var commandLabel = new Label
            {
                Text = $"Turn {_serviceProvider.GlobalState.Turn} - {command.Name}",
                GridRow = _count
            };
            _commandLog.AddChild(commandLabel);
            _count++;
        }
    }
}

