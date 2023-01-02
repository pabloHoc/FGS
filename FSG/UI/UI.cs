using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Commands;
using Myra;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class UI
    {
        private readonly Desktop _desktop = new Desktop();
        private readonly ServiceProvider _serviceProvider;
        private readonly UIEventManager _eventManager = new UIEventManager();

        private SidebarController _sidebar;
        private DebugPanelController _debugPanel;
        private TurnPanelController _turnPanel;

        public UI(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
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

            _desktop.Widgets.Add(_sidebar.Root);
            _desktop.Widgets.Add(_debugPanel.Root);
            _desktop.Widgets.Add(_turnPanel.Root);
        }

        public void Draw()
        {
            _desktop.Render();
        }
    }
}

