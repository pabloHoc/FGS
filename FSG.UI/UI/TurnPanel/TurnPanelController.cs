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

        public TurnPanelController(UIServiceProvider uiServiceProvider)
            : base("../../../UI/TurnPanel/TurnPanel.xaml", uiServiceProvider)
        {
            _turnLabel = (Label)Root.FindWidgetById("TurnLabel");
            var endTurnBtn = (TextButton)Root.FindWidgetById("EndTurnButton");
            endTurnBtn.Click += HandleEndTurn;
        }

        private void HandleEndTurn(object sender, System.EventArgs e)
        {
            _serviceProvider.Dispatcher.Dispatch(new EndTurn());
        }

        public override void Update()
        {
            var turn = _serviceProvider.GlobalState.Turn;
            _turnLabel.Text = $"Turn #{turn}";
        }
    }
}

