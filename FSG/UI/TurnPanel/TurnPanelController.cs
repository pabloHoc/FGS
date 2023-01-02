using System.IO;
using FSG.Core;
using FSG.Commands;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class TurnPanelController : UIController
    {
        private readonly Label _turnLabel;

        public TurnPanelController(ServiceProvider serviceProvider, UIEventManager eventManager, AssetManager assetManager)
            : base("../../../UI/TurnPanel/TurnPanel.xaml", serviceProvider, eventManager, assetManager)
        {
            _turnLabel = (Label)Root.FindWidgetById("TurnLabel");
            var endTurnBtn= (TextButton)Root.FindWidgetById("EndTurnButton");
            endTurnBtn.Click += HandleEndTurn;
        }

        private void HandleEndTurn(object sender, System.EventArgs e)
        {
            _serviceProvider.Dispatcher.Dispatch(new EndTurn());
        }

        public override void Update(ICommand command)
        {
            var turn = _serviceProvider.GlobalState.Turn;
            _turnLabel.Text = $"Turn #{turn}";
        }
    }
}

