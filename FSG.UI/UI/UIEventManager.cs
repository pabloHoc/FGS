using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.UI
{
	public class UIEventManager
	{
        public event EventHandler<Empire> OnEmpireSelected;

        public event EventHandler<Region> OnRegionSelected;

        // TODO: change name
        public event EventHandler<Region> OnRegionSecondaryAction;

        public event EventHandler<Land> OnLandSelected;

        public event EventHandler<Agent> OnAgentSelected;

        public Empire SelectedEmpire { get; private set; }

        public Region SelectedRegion { get; private set; }

        public Land SelectedLand { get; private set; }

        public Agent SelectedAgent { get; private set; }

        private ServiceProvider _serviceProvider;

        public UIEventManager(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private void ClearLand()
        {
            if (SelectedLand != null && SelectedLand.Region != SelectedRegion)
            {
                SelectedLand = null;
            }
        }

        private void UpdateEmpireFromRegion()
        {
            if (SelectedRegion.Empire != SelectedEmpire)
            {
                SelectedEmpire = SelectedRegion.Empire;
            }
        }

        private void UpdateRegion(Region region)
        {
            if (region != SelectedRegion)
            {
                SelectedRegion = region;

                UpdateEmpireFromRegion();
                ClearLand();
            }
        }

        public void SelectEmpire(string empireId)
        {
            if (empireId != SelectedEmpire?.Id)
            {
                SelectedEmpire = _serviceProvider.GlobalState.World.Empires
                    .Find(empire => empire.Id == empireId);

                // Clear Region and Land
                if (SelectedRegion?.Empire != SelectedEmpire)
                {
                    SelectedRegion = null;
                    SelectedLand = null;
                }
            }

            OnEmpireSelected?.Invoke(null, SelectedEmpire);
        }

        public void SelectRegion(string regionId)
        {
            var region = _serviceProvider.GlobalState.World.Regions
                .Find(region => region.Id == regionId);

            UpdateRegion(region);

            OnRegionSelected?.Invoke(null, SelectedRegion);
        }

        // TODO: change name
        public void SecondarySelectRegion(string regionId)
        {
            var region = _serviceProvider.GlobalState.World.Regions
                .Find(region => region.Id == regionId);

            OnRegionSecondaryAction?.Invoke(null, region);
        }

        public void SelectLand(string landId)
        {
            if (landId != SelectedLand?.Id)
            {
                SelectedLand = SelectedRegion.Lands.Find(land => land.Id == landId);

                UpdateRegion(SelectedRegion);
            }

            OnLandSelected?.Invoke(null, SelectedLand);
        }

        public void SelectAgent(string agentId)
        {
            if (agentId != SelectedAgent?.Id)
            {
                SelectedAgent = _serviceProvider.GlobalState.World.Agents
                    .Find(agent => agent.Id == agentId);

                UpdateRegion(SelectedAgent.Region);
            }

            OnAgentSelected?.Invoke(null, SelectedAgent);
        }
    }
}

