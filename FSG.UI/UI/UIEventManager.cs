using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.UI
{
    // TODO: maybe we should store entities instead of ids so we are not searching
    // for them every time

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
            if (SelectedLand != null && SelectedLand.RegionId != SelectedRegion.Id)
            {
                SelectedLand = null;
            }
        }

        private void UpdateEmpireFromRegion()
        {
            if (SelectedRegion.EmpireId != null && SelectedRegion.EmpireId != SelectedEmpire?.Id)
            {
                SelectedEmpire = _serviceProvider.GlobalState.Entities
                    .Get<Empire>(new EntityId<Empire>(SelectedRegion.EmpireId));
            }
            else
            {
                SelectedEmpire = null;
            }
        }

        private void UpdateRegion(EntityId<Region> RegionId)
        {
            if (RegionId != SelectedRegion?.Id)
            {
                SelectedRegion = _serviceProvider.GlobalState.Entities
                    .Get<Region>(RegionId);

                UpdateEmpireFromRegion();
                ClearLand();
            }
        }

        public void SelectEmpire(string empireId)
        {
            if (empireId != SelectedEmpire?.Id)
            {
                SelectedEmpire = _serviceProvider.GlobalState.Entities
                    .Get<Empire>(new EntityId<Empire>(empireId));

                // Clear Region and Land
                if (SelectedRegion.EmpireId != SelectedEmpire.Id)
                {
                    SelectedRegion = null;
                    SelectedLand = null;
                }
            }

            OnEmpireSelected?.Invoke(null, SelectedEmpire);
        }

        public void SelectRegion(string regionId)
        {
            UpdateRegion(new EntityId<Region>(regionId));

            OnRegionSelected?.Invoke(null, SelectedRegion);
        }

        // TODO: change name
        public void SecondarySelectRegion(string regionId)
        {
            var region = _serviceProvider.GlobalState.Entities
                .Get<Region>(new EntityId<Region>(regionId));

            OnRegionSecondaryAction?.Invoke(null, region);
        }

        public void SelectLand(string landId)
        {
            if (landId != SelectedLand?.Id)
            {
                SelectedLand = _serviceProvider.GlobalState.Entities
                    .Get<Land>(new EntityId<Land>(landId));

                UpdateRegion(SelectedLand.RegionId);
            }

            OnLandSelected?.Invoke(null, SelectedLand);
        }

        public void SelectAgent(string agentId)
        {
            if (agentId != SelectedAgent?.Id)
            {
                SelectedAgent = _serviceProvider.GlobalState.Entities
                    .Get<Agent>(new EntityId<Agent>(agentId));

                UpdateRegion(SelectedAgent.RegionId);
            }

            OnAgentSelected?.Invoke(null, SelectedAgent);
        }
    }
}

