using System;
using System.Collections.Generic;
using FSG.Commands;
using FSG.Core;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class SidebarController : UIController
    {
        private readonly EmpireInfoController _empireInfo;
        private readonly RegionInfoController _regionInfo;
        private readonly LandInfoController _landInfo;
        private readonly AgentInfoController _agentInfo;
        private readonly TabsController _tabs;

        public SidebarController(ServiceProvider serviceProvider, UIEventManager eventManager, AssetManager assetManager)
            : base("../../../UI/Sidebar/Sidebar.xaml", serviceProvider, eventManager, assetManager)
        {
            _empireInfo = new EmpireInfoController(serviceProvider, eventManager, assetManager);
            _regionInfo = new RegionInfoController(serviceProvider, eventManager, assetManager);
            _landInfo = new LandInfoController(serviceProvider, eventManager, assetManager);
            _agentInfo = new AgentInfoController(serviceProvider, eventManager, assetManager);

            _tabs = new TabsController(serviceProvider, eventManager, assetManager,
                new Dictionary<string, Widget>
                {
                    { "Empire", _empireInfo.Root },
                    { "Region", _regionInfo.Root },
                    { "Land", _landInfo.Root },
                    { "Agent", _agentInfo.Root },
                });

            ((Panel)Root).Widgets.Add(_tabs.Root);

            eventManager.OnEmpireSelected += HandleEmpireSelected;
            eventManager.OnRegionSelected += HandleRegionSelected;
            eventManager.OnLandSelected += HandleLandSelected;
            eventManager.OnAgentSelected += HandleAgentSelected;
        }

        private void HandleEmpireSelected(object sender, string e)
        {
            _tabs.SwitchTo("Empire");
        }

        private void HandleRegionSelected(object sender, string e)
        {
            _tabs.SwitchTo("Region");
        }

        private void HandleLandSelected(object sender, string e)
        {
            _tabs.SwitchTo("Land");
        }

        private void HandleAgentSelected(object sender, string e)
        {
            _tabs.SwitchTo("Agent");
        }

        public override void Update(ICommand command)
        {
            _empireInfo.Update(command);
            _regionInfo.Update(command);
            _landInfo.Update(command);
            _agentInfo.Update(command);
        }
    }
}

