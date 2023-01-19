using System.IO;
using FSG.Core;
using FSG.Commands;
using FSG.Entities;
using Myra.Assets;
using Myra.Graphics2D.UI;
using System.Collections.Generic;

namespace FSG.UI
{
    public class DebugPanelController : UIController
    {
        private readonly CommandLogController _commandLog;
        private readonly EntityListController<Region> _empireList;
        private readonly EntityListController<Region> _regionList;
        private readonly EntityListController<Agent> _agentList;

        public DebugPanelController(ServiceProvider serviceProvider, UIEventManager eventManager, AssetManager assetManager)
            : base("../../../UI/DebugPanel/DebugPanel.xaml", serviceProvider, eventManager, assetManager)
        {
            _commandLog = new CommandLogController(serviceProvider, eventManager, assetManager);
            _empireList = new EntityListController<Region>(serviceProvider, eventManager, assetManager);
            _regionList = new EntityListController<Region>(serviceProvider, eventManager, assetManager);
            _agentList = new EntityListController<Agent>(serviceProvider, eventManager, assetManager);

            _empireList.EntityClickHandler = eventManager.SelectEmpire;
            _regionList.EntityClickHandler = eventManager.SelectRegion;
            _agentList.EntityClickHandler = eventManager.SelectAgent;

            var tabs = new TabsController(serviceProvider, eventManager, assetManager, new Dictionary<string, UIController>
            {
                { "Commands", _commandLog },
                { "Empires", _empireList },
                { "Regions", _regionList },
                { "Agents", _agentList },
            });

            ((Panel)Root).Widgets.Add(tabs.Root);
        }

        public void OnCommandDispatched(ICommand command)
        {
            _commandLog.Update(command);
        }

        public override void Update()
        {
            _empireList.Update();
            _regionList.Update();
            _agentList.Update();
        }
    }
}

