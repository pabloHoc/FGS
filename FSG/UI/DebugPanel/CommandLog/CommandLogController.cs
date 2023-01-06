using System.IO;
using FSG.Core;
using FSG.Commands;
using Myra.Assets;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework;

namespace FSG.UI
{
    public class CommandLogController : UIController
    {
        private readonly Grid _commandLog;
        private readonly ScrollViewer _content;
        private int _count = 1;

        public CommandLogController(ServiceProvider serviceProvider, UIEventManager eventManager, AssetManager assetManager)
            : base("../../../UI/DebugPanel/CommandLog/CommandLog.xaml", serviceProvider, eventManager, assetManager)
        {
            _commandLog = (Grid)Root.FindWidgetById("CommandLogGrid");
            _content = (ScrollViewer)Root.FindWidgetById("CommandLogScrollViewer");
        }

        public override void Update(ICommand command)
        {
            var commandLabel = new Label();
            commandLabel.Text = command.Name;
            commandLabel.GridRow = _count;
            _commandLog.AddChild(commandLabel);
            _count++;
        }
    }
}

