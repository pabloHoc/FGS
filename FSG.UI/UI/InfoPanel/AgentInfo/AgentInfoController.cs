using FSG.Commands;
using FSG.Definitions;
using FSG.Entities;
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

        private readonly HorizontalStackPanel _actionPanel;

        public AgentInfoController(UIServiceProvider uiServiceProvider)
            : base("../../../UI/InfoPanel/AgentInfo/AgentInfo.xaml", uiServiceProvider)
        {
            _agentNameLabel = (Label)Root.FindWidgetById("AgentNameLabel");
            _currentActionLabel = (Label)Root.FindWidgetById("CurrentActionLabel");
            _currentRegionLabel = (Label)Root.FindWidgetById("CurrentRegionLabel");
            _empireLabel = (Label)Root.FindWidgetById("EmpireLabel");
            _spellPanel = (HorizontalStackPanel)Root.FindWidgetById("SpellPanel");
            _actionPanel = (HorizontalStackPanel)Root.FindWidgetById("ActionPanel");
            _uiServiceProvider.EventManager.OnAgentSelected += HandleAgentSelected;
        }

        private void HandleAgentSelected(object sender, Agent agentId)
        {
            Update();
        }

        private void HandleSpellClick(object sender, System.EventArgs e)
        {
            var spellBtn = (TextButton)sender;
            var spellDefinition = _serviceProvider.Definitions.Get<SpellDefinition>(spellBtn.Id);

            _serviceProvider.Dispatcher.Dispatch(new SetEntityCurrentAction
            {
                EntityId = _uiServiceProvider.EventManager.SelectedAgent.Id,
                EntityType = EntityType.Agent,
                NewCurrentAction = new ActionQueueItem
                {
                    ActionType = ActionType.Spell,
                    Name = spellDefinition.Name,
                    RemainingTurns = spellDefinition.BaseExecutionTime
                }
            });
        }

        private void HandleActionClick(object sender, System.EventArgs e)
        {
            var actionBtn = (TextButton)sender;
            var actionDefinition = _serviceProvider.Definitions.Get<AgentActionDefinition>(actionBtn.Id);

            _serviceProvider.Dispatcher.Dispatch(new SetEntityCurrentAction
            {
                EntityId = _uiServiceProvider.EventManager.SelectedAgent.Id,
                EntityType = EntityType.Agent,
                NewCurrentAction = new ActionQueueItem
                {
                    ActionType = ActionType.Action,
                    Name = actionDefinition.Name,
                    RemainingTurns = actionDefinition.BaseExecutionTime
                }
            });
        }

        private void HandleRegionClick(object sender, System.EventArgs e)
        {
            var label = (Label)sender;
            _uiServiceProvider.EventManager.SelectRegion(label.Id);
        }

        private void HandleEmpireClick(object sender, System.EventArgs e)
        {
            var label = (Label)sender;
            _uiServiceProvider.EventManager.SelectEmpire(label.Id);
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

        private void UpdateActionList(Agent agent)
        {
            _actionPanel.Widgets.Clear();

            var actionDefinitions = _serviceProvider.Definitions.GetAll<AgentActionDefinition>();

            foreach (var action in actionDefinitions)
            {
                var actionBtn = new TextButton
                {
                    Id = action.Name,
                    Text = action.Name,
                    Enabled = _serviceProvider.Services.ActionService.Allow(agent, action)
                };
                actionBtn.Click += HandleActionClick;

                _actionPanel.Widgets.Add(actionBtn);
            }
        }

        private void UpdateRegion(Agent agent)
        {
            _currentRegionLabel.Id = agent.Region.Id;
            _currentRegionLabel.Text = agent.Region.Name;
            _currentRegionLabel.TouchDown += HandleRegionClick;
        }

        private void UpdateEmpire(Agent agent)
        {
            _empireLabel.Id = agent.Empire.Id;
            _empireLabel.Text = agent.Empire.Name;
            _empireLabel.TouchDown += HandleEmpireClick;
        }

        public override void Update()
        {
            if (_uiServiceProvider.EventManager.SelectedAgent != null)
            {
                var agent = _uiServiceProvider.EventManager.SelectedAgent;

                _agentNameLabel.Text = agent.Name;
                _currentActionLabel.Text = agent.Actions.Count > 0 ?
                    $"{agent.Actions.Peek().Name} ({agent.Actions.Peek().RemainingTurns})" : "";

                UpdateSpellList(agent);
                UpdateActionList(agent);
                UpdateEmpire(agent);
                UpdateRegion(agent);
            }
        }
    }
}

