using System.IO;
using FSG.Core;
using FSG.Commands;
using FSG.Entities;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class DebugPanelController : UIController
    {
        private readonly CommandLogController _commandLog;
        private readonly EntityListController<Empire> _empireList;
        private readonly EntityListController<Region> _regionList;
        private readonly EntityListController<Agent> _agentList;
        private readonly string[] _tabs = new string[] { "CommandsTab", "EmpiresTab", "RegionsTab", "AgentsTab" };
        private readonly Widget[] _tabItems = new Widget[4];
        private readonly Panel _tabItemsPanel;

        public DebugPanelController(ServiceProvider serviceProvider, Desktop desktop, UIEventManager eventManager, AssetManager assetManager)
            : base("../../../UI/DebugPanel/DebugPanel.xaml", serviceProvider, desktop, eventManager, assetManager)
        {
            _commandLog = new CommandLogController(serviceProvider, desktop, eventManager, assetManager);
            _empireList = new EntityListController<Empire>(serviceProvider, desktop, eventManager, assetManager);
            _regionList = new EntityListController<Region>(serviceProvider, desktop, eventManager, assetManager);
            _agentList = new EntityListController<Agent>(serviceProvider, desktop, eventManager, assetManager);

            _empireList.EntityClickHandler = eventManager.SelectEmpire;
            _regionList.EntityClickHandler = eventManager.SelectRegion;

            foreach (var tab in _tabs)
            {
                ((TextButton)Root.FindWidgetById(tab)).Click += handleTabClick;
            }

            _tabItems[0] = _commandLog.Root;
            _tabItems[1] = _empireList.Root;
            _tabItems[2] = _regionList.Root;
            _tabItems[3] = _agentList.Root;

            _tabItemsPanel = (Panel)Root.FindWidgetById("TabItems");
            _tabItemsPanel.Widgets.Add(_tabItems[0]);
        }

        private void handleTabClick(object sender, System.EventArgs e)
        {
            for (int i = 0; i < _tabs.Length; i++)
            {
                if(((TextButton)sender).Id == _tabs[i])
                {
                    if (!_tabItemsPanel.Widgets.Contains(_tabItems[i]))
                    {
                        _tabItemsPanel.Widgets.Add(_tabItems[i]);
                    }
                } else
                {
                    if (_tabItemsPanel.Widgets.Contains(_tabItems[i]))
                    {
                        _tabItemsPanel.Widgets.Remove(_tabItems[i]);
                    }
                }
            }
        }

        public override void Update(ICommand command)
        {
            _commandLog.Update(command);
            _empireList.Update(command);
            _regionList.Update(command);
            _agentList.Update(command);
        }
    }
}

