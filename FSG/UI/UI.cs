﻿using System;
using System.Collections.Generic;
using FSG.Core;
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

        public UI(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _serviceProvider.Dispatcher.CommandDispatched += OnCommandDispatched;
        }

        private void OnCommandDispatched(object sender, Commands.ICommand e)
        {
            Update();
        }

        public void Initialize()
        {
            var assetResolver = new FileAssetResolver("../../../UI");
            var assetManager = new AssetManager(assetResolver);

            _controllers.Add(new EmpireDetailsController(_serviceProvider, _desktop, assetManager));
            _controllers.Add(new TurnPanelController(_serviceProvider, _desktop, assetManager));
        }

        public void Draw()
        {
            _desktop.Render();
        }

        private void Update()
        {
            foreach(var controller in _controllers)
            {
                controller.Update();
            }
        }
    }
}
