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
        private readonly Desktop _desktop = new Desktop();

        private readonly ServiceProvider _serviceProvider;

        private readonly UIEventManager _eventManager;

        private readonly GraphicsDevice _graphicsDevice;

        private readonly SpriteBatch _spriteBatch;

        // Components

        private SidebarController _sidebar;

        private DebugPanelController _debugPanel;

        private TurnPanelController _turnPanel;

        // other

        public UI(ServiceProvider serviceProvider, UIEventManager eventManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            _serviceProvider = serviceProvider;
            _eventManager = eventManager;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = spriteBatch;

            _serviceProvider.Dispatcher.OnCommandDispatched += HandleCommandDispatched;
            _serviceProvider.Dispatcher.OnCommandProcessed += HandleCommandProcessed;
        }

        private void HandleCommandDispatched(object sender, ICommand command)
        {
            _debugPanel.OnCommandDispatched(command);
        }

        private void HandleCommandProcessed(object sender, ICommand command)
        {
            _sidebar.Update(command);
            _debugPanel.Update(command);
            _turnPanel.Update(command);
        }

        public void Initialize()
        {
            var assetResolver = new FileAssetResolver("../../../UI");
            var assetManager = new AssetManager(assetResolver);

            _sidebar = new SidebarController(_serviceProvider, _eventManager, assetManager);
            _debugPanel = new DebugPanelController(_serviceProvider, _eventManager, assetManager);
            _turnPanel = new TurnPanelController(_serviceProvider, _eventManager, assetManager);

            var verticalPanel = new VerticalStackPanel();
            verticalPanel.Proportions.Add(new Proportion { Type = ProportionType.Fill });

            verticalPanel.Widgets.Add(_sidebar.Root);
            verticalPanel.Widgets.Add(_debugPanel.Root);
            verticalPanel.Widgets.Add(_turnPanel.Root);

            _desktop.Widgets.Add(verticalPanel);
        }

        public void Draw()
        {
            _desktop.Render();
        }
    }
}

