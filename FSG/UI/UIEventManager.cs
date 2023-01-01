using System;
namespace FSG.UI
{
	public class UIEventManager
	{
        public event EventHandler<string> OnEmpireSelected;

        public event EventHandler<string> OnRegionSelected;

        public void SelectEmpire(string empireId)
        {
            OnEmpireSelected?.Invoke(null, empireId);
        }

        public void SelectRegion(string regionId)
        {
            OnRegionSelected?.Invoke(null, regionId);
        }
	}
}

