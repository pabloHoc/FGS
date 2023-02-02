using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Commands;
using Myra;
using Myra.Assets;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace FSG.UI
{
    public class UI
    {
        public EventHandler OnUIMouseEntered;

        public EventHandler OnUIMouseLeft;

        //
        private readonly Desktop _desktop = new Desktop();

        private readonly UIServiceProvider _uiServiceProvider;

        // Components

        private TopbarController _topbar;

        private PanelInfoController _infoPanel;

        private DebugPanelController _debugPanel;

        private TurnPanelController _turnPanel;

        private Window _debugWindow;

        private Window _infoWindow;

        // other

        public UI(UIServiceProvider uiServiceProvider)
        {
            _uiServiceProvider = uiServiceProvider;

            _uiServiceProvider.GameServiceProvider.Dispatcher.OnCommandDispatched += HandleCommandDispatched;
            _uiServiceProvider.GameServiceProvider.Dispatcher.OnCommandProcessed += HandleCommandProcessed;

            _uiServiceProvider.EventManager.OnRegionSelected += HandleRegionSelected;
        }

        private void HandleRegionSelected(object sender, Entities.Region e)
        {
            if (!_desktop.Widgets.Contains(_debugWindow))
            {
                _debugWindow.Show(_desktop);
            }

            if (!_desktop.Widgets.Contains(_infoWindow))
            {
                _infoWindow.Show(_desktop);
            }
        }

        private void HandleCommandDispatched(object sender, ICommand command)
        {
            _debugPanel.OnCommandDispatched(command);
        }

        private void HandleCommandProcessed(object sender, ICommand command)
        {
            _topbar.Update();
            _infoPanel.Update();
            _debugPanel.Update(command);
            _turnPanel.Update();
        }

        public void Initialize()
        {
            _topbar = new TopbarController(_uiServiceProvider);
            _infoPanel = new PanelInfoController(_uiServiceProvider);
            _debugPanel = new DebugPanelController(_uiServiceProvider);
            _turnPanel = new TurnPanelController(_uiServiceProvider);

            _topbar.Root.MouseEntered += HandleUIMouseEnter;
            _topbar.Root.MouseLeft += HandleUIMouseLeave;
            _turnPanel.Root.MouseEntered += HandleUIMouseEnter;
            _turnPanel.Root.MouseLeft += HandleUIMouseLeave;

            _debugWindow = new Window
            {
                Title = "Debug Panel"
            };

            _debugWindow.Content = _debugPanel.Root;
            _debugWindow.Show(_desktop);
            _debugWindow.MouseEntered += HandleUIMouseEnter;
            _debugWindow.MouseLeft += HandleUIMouseLeave;

            _infoWindow = new Window
            {
                Title = "Info Panel"
            };

            _infoWindow.Content = _infoPanel.Root;
            _infoWindow.Show(_desktop);
            _infoWindow.MouseEntered += HandleUIMouseEnter;
            _infoWindow.MouseLeft += HandleUIMouseLeave;

            _topbar.Root.MouseEntered += HandleUIMouseEnter;
            _topbar.Root.MouseLeft += HandleUIMouseLeave;

            _desktop.Widgets.Add(_topbar.Root);
            _desktop.Widgets.Add(_turnPanel.Root);
        }

        private void HandleUIMouseEnter(object sender, EventArgs e)
        {
            OnUIMouseEntered?.Invoke(sender, e);
        }

        private void HandleUIMouseLeave(object sender, EventArgs e)
        {
            OnUIMouseLeft?.Invoke(sender, e);
        }

        public void Draw()
        {
            _desktop.Render();
        }

        public void Update(GameTime gameTime)
        {
            _turnPanel.Update(gameTime);
        }
    }
}

