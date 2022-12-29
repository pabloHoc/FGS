using System.IO;
using FSG.Core;
using FSG.Commands;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class TurnPanelController : UIController
    {
        public TurnPanelController(ServiceProvider serviceProvider, Desktop desktop, AssetManager assetManager)
            : base("../../../UI/TurnPanel/TurnPanel.xaml", serviceProvider, desktop, assetManager)
        {
            var endTurnBtn= (TextButton)_root.FindWidgetById("EndTurnButton");
            endTurnBtn.Click += HandleEndTurn;
        }

        private void HandleEndTurn(object sender, System.EventArgs e)
        {
            _serviceProvider.Dispatcher.Dispatch(new EndTurn());
        }

        public override void Update()
        {
            var turnLabel = (Label)_root.FindWidgetById("TurnLabel");
            var turn = _serviceProvider.GlobalState.Turn;
            turnLabel.Text = $"Turn #{turn}";
        }
    }
}

