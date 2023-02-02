using System.IO;
using FSG.Core;
using FSG.Commands;
using Myra.Assets;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework;

namespace FSG.UI
{
    public class TurnPanelController : UIController
    {
        private readonly Label _turnLabel;

        private double _elapsedTime;

        private double _speedModifier = 1;

        private bool _isPaused = false;

        private readonly TextButton _pauseContinueBtn;

        private readonly Label _speedLabel;

        public TurnPanelController(UIServiceProvider uiServiceProvider)
            : base("../../../UI/TurnPanel/TurnPanel.xaml", uiServiceProvider)
        {
            _turnLabel = (Label)Root.FindWidgetById("TurnLabel");
            var endTurnBtn = (TextButton)Root.FindWidgetById("EndTurnButton");
            var reduceSpeedBtn = (TextButton)Root.FindWidgetById("ReduceSpeedButton");
            var increaseSpeedBtn = (TextButton)Root.FindWidgetById("IncreaseSpeedButton");
            _pauseContinueBtn = (TextButton)Root.FindWidgetById("PauseContinueButton");
            _speedLabel = (Label)Root.FindWidgetById("SpeedLabel");

            endTurnBtn.Click += HandleEndTurn;
            reduceSpeedBtn.Click += HandleReduceSpeed;
            increaseSpeedBtn.Click += HandleIncreaseSpeed;
            _pauseContinueBtn.Click += HandlePauseContinue;
        }

        private void HandleEndTurn(object sender, System.EventArgs e)
        {
            _serviceProvider.Dispatcher.Dispatch(new EndTurn());
        }

        private void HandleReduceSpeed(object sender, System.EventArgs e)
        {
            if (_speedModifier > 0.5)
            {
                _speedModifier -= 0.5;
            }
            _speedLabel.Text = $"Speed: {_speedModifier}x";
        }

        private void HandleIncreaseSpeed(object sender, System.EventArgs e)
        {
            if (_speedModifier < 3)
            {
                _speedModifier += 0.5;
            }
            _speedLabel.Text = $"Speed: {_speedModifier}x";
        }

        private void HandlePauseContinue(object sender, System.EventArgs e)
        {
            _isPaused = !_isPaused;
            _pauseContinueBtn.Text = _isPaused ? "Continue" : "Pause";
        }

        public void Update(GameTime gameTime)
        {
            if (!_isPaused)
            {
                _elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

                if (_elapsedTime >= 1000 / _speedModifier)
                {
                    _elapsedTime = 0;
                    _serviceProvider.Dispatcher.Dispatch(new EndTurn());
                }
            }

            var turn = _serviceProvider.GlobalState.Turn;
            _turnLabel.Text = $"Turn #{turn}";
        }
    }
}

