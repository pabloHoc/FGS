using System;
using System.Collections.Generic;
using FSG.Commands;
using FSG.Core;
using FSG.Entities;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class SidebarController : UIController
    {
        private readonly EmpireInfoController _empireInfo;
        private readonly RegionInfoController _regionInfo;
        private readonly LandInfoController _landInfo;
        private readonly CapitalInfoController _capitalInfo;
        private readonly PopulationInfoController _populationInfo;
        private readonly AgentInfoController _agentInfo;
        private readonly TabsController _tabs;

        public SidebarController(ServiceProvider serviceProvider, UIEventManager eventManager, AssetManager assetManager)
            : base("../../../UI/Sidebar/Sidebar.xaml", serviceProvider, eventManager, assetManager)
        {
            _empireInfo = new EmpireInfoController(serviceProvider, eventManager, assetManager);
            _regionInfo = new RegionInfoController(serviceProvider, eventManager, assetManager);
            _landInfo = new LandInfoController(serviceProvider, eventManager, assetManager);
            _capitalInfo = new CapitalInfoController(serviceProvider, eventManager, assetManager);
            _populationInfo = new PopulationInfoController(serviceProvider, eventManager, assetManager);

            _agentInfo = new AgentInfoController(serviceProvider, eventManager, assetManager);

            _tabs = new TabsController(serviceProvider, eventManager, assetManager,
                new Dictionary<string, UIController>
                {
                    { "Empire", _empireInfo },
                    { "Region", _regionInfo },
                    { "Land", _landInfo },
                    { "Capital", _capitalInfo },
                    { "Population", _populationInfo },
                    { "Agent", _agentInfo },
                });

            ((Panel)Root).Widgets.Add(_tabs.Root);

            eventManager.OnEmpireSelected += HandleEmpireSelected;
            eventManager.OnRegionSelected += HandleRegionSelected;
            eventManager.OnLandSelected += HandleLandSelected;
            eventManager.OnAgentSelected += HandleAgentSelected;
        }

        private void HandleEmpireSelected(object sender, Empire e)
        {
            _tabs.SwitchTo("Empire");
        }

        private void HandleRegionSelected(object sender, Region e)
        {
            _tabs.SwitchTo("Region");
        }

        private void HandleLandSelected(object sender, Land e)
        {
            _tabs.SwitchTo("Land");
        }

        private void HandleAgentSelected(object sender, Agent e)
        {
            _tabs.SwitchTo("Agent");
        }

        public override void Update(ICommand command)
        {
            _empireInfo.Update();
            _regionInfo.Update();
            _landInfo.Update();
            _capitalInfo.Update();
            _populationInfo.Update();
            _agentInfo.Update();
        }
    }
}

