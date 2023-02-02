using FSG.Entities;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class PopulationInfoController : UIController
    {
        private readonly VerticalStackPanel _popList;

        public PopulationInfoController(UIServiceProvider uiServiceProvider)
            : base("../../../UI/InfoPanel/PopulationInfo/PopulationInfo.xaml", uiServiceProvider)
        {
            _popList = (VerticalStackPanel)Root.FindWidgetById("PopList");

            _uiServiceProvider.EventManager.OnRegionSelected += HandleRegionSelected;
        }

        private void HandleRegionSelected(object sender, Region region)
        {
            Update();
        }

        private void UpdatePops(Region region)
        {
            _popList.Widgets.Clear();

            foreach (var pop in region.Pops)
            {
                _popList.Widgets.Add(new Label
                {
                    Text = $"{pop.Strata} ({pop.Size}) - GP: {pop.GrowthPoints}"
                });
            }
        }

        private void Clear()
        {
            _popList.Widgets.Clear();
        }

        public override void Update()
        {
            if (_uiServiceProvider.EventManager.SelectedRegion?.Empire != null)
            {
                UpdatePops(_uiServiceProvider.EventManager.SelectedRegion);
            } else
            {
                Clear();
            }
        }
    }
}

