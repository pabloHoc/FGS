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

        public DebugPanelController(UIServiceProvider uiServiceProvider)
            : base("../../../UI/DebugPanel/DebugPanel.xaml", uiServiceProvider)
        {
            _commandLog = new CommandLogController(_uiServiceProvider);
            _empireList = new EntityListController<Empire>(_serviceProvider.GlobalState.World.Empires, _uiServiceProvider);
            _regionList = new EntityListController<Region>(_serviceProvider.GlobalState.World.Regions, _uiServiceProvider);
            _agentList = new EntityListController<Agent>(_serviceProvider.GlobalState.World.Agents, _uiServiceProvider);

            _empireList.EntityClickHandler = _uiServiceProvider.EventManager.SelectEmpire;
            _regionList.EntityClickHandler = _uiServiceProvider.EventManager.SelectRegion;
            _agentList.EntityClickHandler = _uiServiceProvider.EventManager.SelectAgent;

            var tabs = new TabsController(_uiServiceProvider, new Dictionary<string, UIController>
            {
                { "Empires", _empireList },
                { "Regions", _regionList },
                { "Agents", _agentList },
                { "Commands", _commandLog },
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

