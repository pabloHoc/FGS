using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Commands;
using Myra;
using Myra.Assets;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FSG.UI
{
    public class UI
    {
        private readonly Desktop _desktop = new Desktop();

        private readonly ServiceProvider _serviceProvider;

        private readonly UIEventManager _eventManager = new UIEventManager();

        private readonly GraphicsDevice _graphicsDevice;

        private readonly SpriteBatch _spriteBatch;

        // Components

        private SidebarController _sidebar;

        private DebugPanelController _debugPanel;

        private TurnPanelController _turnPanel;

        private MapController _map;

        // other

        public UI(ServiceProvider serviceProvider, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            _serviceProvider = serviceProvider;
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
            _map = new MapController(_serviceProvider, _eventManager, _graphicsDevice, _spriteBatch);

            _desktop.Widgets.Add(_sidebar.Root);
            _desktop.Widgets.Add(_debugPanel.Root);
            _desktop.Widgets.Add(_turnPanel.Root);
        }

        public void Draw()
        {
            //_desktop.Render();
            _map.Update();
        }
    }
}

