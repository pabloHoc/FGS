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
        private readonly List<UIController> _controllers = new List<UIController>();
        private readonly ServiceProvider _serviceProvider;
        private readonly UIEventManager _eventManager = new UIEventManager();

        public UI(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _serviceProvider.Dispatcher.CommandDispatched += OnCommandDispatched;
        }

        private void OnCommandDispatched(object sender, ICommand command)
        {
            Update(command);
        }

        public void Initialize()
        {
            var assetResolver = new FileAssetResolver("../../../UI");
            var assetManager = new AssetManager(assetResolver);

            _controllers.Add(new EmpireInfoController(_serviceProvider, _desktop, _eventManager, assetManager));
            _controllers.Add(new DebugPanelController(_serviceProvider, _desktop, _eventManager, assetManager));
            _controllers.Add(new TurnPanelController(_serviceProvider, _desktop, _eventManager, assetManager));
        }

        public void Draw()
        {
            _desktop.Render();
        }

        private void Update(ICommand command)
        {
            foreach (var controller in _controllers)
            {
                controller.Update(command);
            }
        }
    }
}

