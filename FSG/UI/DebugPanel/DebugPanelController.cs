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
        private readonly EntityListController<Empire> _empireList;
        private readonly EntityListController<Region> _regionList;
        private readonly EntityListController<Agent> _agentList;

        public DebugPanelController(ServiceProvider serviceProvider, UIEventManager eventManager, AssetManager assetManager)
            : base("../../../UI/DebugPanel/DebugPanel.xaml", serviceProvider, eventManager, assetManager)
        {
            _commandLog = new CommandLogController(serviceProvider, eventManager, assetManager);
            _empireList = new EntityListController<Empire>(serviceProvider, eventManager, assetManager);
            _regionList = new EntityListController<Region>(serviceProvider, eventManager, assetManager);
            _agentList = new EntityListController<Agent>(serviceProvider, eventManager, assetManager);

            _empireList.EntityClickHandler = eventManager.SelectEmpire;
            _regionList.EntityClickHandler = eventManager.SelectRegion;
            _agentList.EntityClickHandler = eventManager.SelectAgent;

            var tabs = new TabsController(serviceProvider, eventManager, assetManager, new Dictionary<string, Widget>
            {
                { "Commands", _commandLog.Root },
                { "Empires", _empireList.Root },
                { "Regions", _regionList.Root },
                { "Agents", _agentList.Root },
            });

            ((Panel)Root).Widgets.Add(tabs.Root);
        }

        public void OnCommandDispatched(ICommand command)
        {
            _commandLog.Update(command);
        }

        public override void Update(ICommand command)
        {
            _empireList.Update(command);
            _regionList.Update(command);
            _agentList.Update(command);
        }
    }
}

