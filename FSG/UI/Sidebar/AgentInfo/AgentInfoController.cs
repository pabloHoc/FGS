﻿using System.IO;
using FSG.Core;
using FSG.Commands;
using FSG.Entities;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class AgentInfoController : UIController
    {
        private readonly Label _agentNameLabel;

        public AgentInfoController(ServiceProvider serviceProvider, UIEventManager eventManager, AssetManager assetManager)
            : base("../../../UI/Sidebar/AgentInfo/AgentInfo.xaml", serviceProvider, eventManager, assetManager)
        {
            _agentNameLabel = (Label)Root.FindWidgetById("AgentNameLabel");
            _eventManager.OnAgentSelected += handleAgentSelected;
        }

        private void handleAgentSelected(object sender, string agentId)
        {
            Update();
        }

        public override void Update(ICommand command = null)
        {
            if (_eventManager.SelectedAgentId != null)
            {
                var agent = _serviceProvider.GlobalState.Entities.Get<Agent>(new EntityId<Agent>(_eventManager.SelectedAgentId));
                _agentNameLabel.Text = agent.Name;
            }
        }
    }
}
