using System;
using System.Collections.Generic;
using FSG.Commands;
using FSG.Core;
using FSG.Entities;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class PanelInfoController : UIController
    {
        private readonly EmpireInfoController _empireInfo;

        private readonly RegionInfoController _regionInfo;

        private readonly LandInfoController _landInfo;

        private readonly CapitalInfoController _capitalInfo;

        private readonly PopulationInfoController _populationInfo;

        private readonly AgentInfoController _agentInfo;

        private readonly TabsController _tabs;

        public PanelInfoController(UIServiceProvider uiServiceProvider)
            : base("../../../UI/InfoPanel/InfoPanel.xaml", uiServiceProvider)
        {
            _empireInfo = new EmpireInfoController(uiServiceProvider);
            _regionInfo = new RegionInfoController(uiServiceProvider);
            _landInfo = new LandInfoController(uiServiceProvider);
            _capitalInfo = new CapitalInfoController(uiServiceProvider);
            _populationInfo = new PopulationInfoController(uiServiceProvider);

            _agentInfo = new AgentInfoController(uiServiceProvider);

            _tabs = new TabsController(uiServiceProvider,
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

            _uiServiceProvider.EventManager.OnEmpireSelected += HandleEmpireSelected;
            _uiServiceProvider.EventManager.OnRegionSelected += HandleRegionSelected;
            _uiServiceProvider.EventManager.OnLandSelected += HandleLandSelected;
            _uiServiceProvider.EventManager.OnAgentSelected += HandleAgentSelected;
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

        public override void Update()
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

