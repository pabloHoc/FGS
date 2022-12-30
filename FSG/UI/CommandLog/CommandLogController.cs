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
        private readonly VerticalStackPanel _panel;
        private readonly Grid _commandLog;
        private readonly ScrollViewer _content;
        private readonly TextButton _toggleBtn;
        private int _count = 1;
        private bool _show = true;

        public CommandLogController(ServiceProvider serviceProvider, Desktop desktop, AssetManager assetManager)
            : base("../../../UI/CommandLog/CommandLog.xaml", serviceProvider, desktop, assetManager)
        {
            _panel = (VerticalStackPanel)_root.FindWidgetById("CommandLogPanel");
            _commandLog= (Grid)_root.FindWidgetById("CommandLogGrid");
            _content = (ScrollViewer)_root.FindWidgetById("CommandLogScrollViewer");
            _toggleBtn = (TextButton)_root.FindWidgetById("CommandLogBtn");
            _toggleBtn.Click += handleToggle;
        }

        private void handleToggle(object sender, System.EventArgs e)
        {
            if (_show)
            {
                _panel.Widgets.Remove(_content);
                _show = false;
            } else
            {
                _panel.Widgets.Add(_content);
                _show = true;
            }
        }

        public override void Update(ICommand command)
        {
            var commandLabel = new Label();
            commandLabel.Text = command.Action;
            commandLabel.GridRow = _count;
            _commandLog.AddChild(commandLabel);
            _count++;
        }
    }
}

