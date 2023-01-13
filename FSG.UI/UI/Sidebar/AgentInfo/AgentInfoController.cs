using System.IO;
using FSG.Core;
using FSG.Commands;
using FSG.Definitions;
using FSG.Entities;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class AgentInfoController : UIController
    {
        private readonly Label _agentNameLabel;
        private readonly Label _currentActionLabel;
        private readonly Label _currentRegionLabel;
        private readonly Label _empireLabel;
        private readonly HorizontalStackPanel _spellPanel;

        public AgentInfoController(ServiceProvider serviceProvider, UIEventManager eventManager, AssetManager assetManager)
            : base("../../../UI/Sidebar/AgentInfo/AgentInfo.xaml", serviceProvider, eventManager, assetManager)
        {
            _agentNameLabel = (Label)Root.FindWidgetById("AgentNameLabel");
            _currentActionLabel = (Label)Root.FindWidgetById("CurrentActionLabel");
            _currentRegionLabel = (Label)Root.FindWidgetById("CurrentRegionLabel");
            _empireLabel = (Label)Root.FindWidgetById("EmpireLabel");
            _spellPanel = (HorizontalStackPanel)Root.FindWidgetById("SpellPanel");
            _eventManager.OnAgentSelected += HandleAgentSelected;
        }

        private void HandleAgentSelected(object sender, string agentId)
        {
            Update();
        }

        private void HandleSpellClick(object sender, System.EventArgs e)
        {
            var spellBtn = (TextButton)sender;
            var spellDefinition = _serviceProvider.Definitions.Get<SpellDefinition>(spellBtn.Id);

            _serviceProvider.Dispatcher.Dispatch(new SetEntityCurrentAction<Agent>
            {
                EntityId = new EntityId<Agent>(_eventManager.SelectedAgentId),
                EntityType = EntityType.Agent,
                NewCurrentAction = new ActionQueueItem
                {
                    ActionType = ActionType.Spell,
                    Name = spellDefinition.Name,
                    RemainingTurns = spellDefinition.BaseExecutionTime
                }
            });
        }

        private void HandleRegionClick(object sender, System.EventArgs e)
        {
            var label = (Label)sender;
            _eventManager.SelectRegion(label.Id);
        }

        private void HandleEmpireClick(object sender, System.EventArgs e)
        {
            var label = (Label)sender;
            _eventManager.SelectEmpire(label.Id);
        }

        private void UpdateSpellList(Agent agent)
        {
            _spellPanel.Widgets.Clear();

            var spellDefinitions = _serviceProvider.Definitions.GetAll<SpellDefinition>();

            foreach (var spell in spellDefinitions)
            {
                var spellBtn = new TextButton
                {
                    Id = spell.Name,
                    Text = spell.Name,
                    Enabled = _serviceProvider.Services.SpellService.Allow(agent, spell)
                };
                spellBtn.Click += HandleSpellClick;

                _spellPanel.Widgets.Add(spellBtn);
            }
        }

        private void UpdateRegion(Agent agent)
        {
            var region = _serviceProvider.GlobalState.Entities.Get<Region>(agent.RegionId);
            _currentRegionLabel.Id = region.Id;
            _currentRegionLabel.Text = region.Name;
            _currentRegionLabel.TouchDown += HandleRegionClick;
        }

        private void UpdateEmpire(Agent agent)
        {
            var empire = _serviceProvider.GlobalState.Entities.Get<Empire>((EntityId<Empire>)agent.EmpireId);
            _empireLabel.Id = empire.Id;
            _empireLabel.Text = empire.Name;
            _empireLabel.TouchDown += HandleEmpireClick;
        }

        public override void Update(ICommand command = null)
        {
            if (_eventManager.SelectedAgentId != null)
            {
                var agent = _serviceProvider.GlobalState.Entities
                    .Get<Agent>(new EntityId<Agent>(_eventManager.SelectedAgentId));

                _agentNameLabel.Text = agent.Name;
                _currentActionLabel.Text = agent.Actions.Count > 0 ?
                    $"{agent.Actions.Peek().Name} ({agent.Actions.Peek().RemainingTurns})" : "";

                UpdateSpellList(agent);
                UpdateEmpire(agent);
                UpdateRegion(agent);
            }
        }
    }
}

