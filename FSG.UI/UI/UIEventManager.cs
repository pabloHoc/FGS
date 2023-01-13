using System;
namespace FSG.UI
{
	public class UIEventManager
	{
        public event EventHandler<string> OnEmpireSelected;

        public event EventHandler<string> OnRegionSelected;

        public event EventHandler<string> OnLandSelected;

        public event EventHandler<string> OnAgentSelected;

        public string SelectedEmpireId { get; private set; }

        public string SelectedRegionId { get; private set; }

        public string SelectedLandId { get; private set; }

        public string SelectedAgentId { get; private set; }

        public void SelectEmpire(string empireId)
        {
            SelectedEmpireId = empireId;
            OnEmpireSelected?.Invoke(null, empireId);
        }

        public void SelectRegion(string regionId)
        {
            SelectedRegionId = regionId;
            OnRegionSelected?.Invoke(null, regionId);
        }

        public void SelectLand(string landId)
        {
            SelectedLandId = landId;
            OnLandSelected?.Invoke(null, landId);
        }

        public void SelectAgent(string agentId)
        {
            SelectedAgentId = agentId;
            OnAgentSelected?.Invoke(null, agentId);
        }
    }
}

