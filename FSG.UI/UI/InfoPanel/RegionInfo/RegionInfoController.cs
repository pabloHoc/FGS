using FSG.Entities;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class RegionInfoController : UIController
    {
        private readonly Label _regionNameLabel;

        private readonly Label _empireLabel;

        private readonly VerticalStackPanel _resourceList;

        private readonly VerticalStackPanel _buildingQueue;

        private readonly EntityListController<Land> _landList;

        public RegionInfoController(UIServiceProvider uiServiceProvider)
            : base("../../../UI/InfoPanel/RegionInfo/RegionInfo.xaml", uiServiceProvider)
        {
            _regionNameLabel = (Label)Root.FindWidgetById("RegionNameLabel");
            _empireLabel = (Label)Root.FindWidgetById("EmpireLabel");
            _resourceList = (VerticalStackPanel)Root.FindWidgetById("ResourceList");
            _buildingQueue = (VerticalStackPanel)Root.FindWidgetById("BuildingQueue");

            _uiServiceProvider.EventManager.OnRegionSelected += HandleRegionSelected;

            _landList = new EntityListController<Land>(
                _serviceProvider.GlobalState.World.Lands,
                _uiServiceProvider,
                ((land) => land.Region == _uiServiceProvider.EventManager.SelectedRegion)
            )
            {
                EntityClickHandler = uiServiceProvider.EventManager.SelectLand
            };

            var landListPanel = (VerticalStackPanel)Root.FindWidgetById("LandList");
            landListPanel.Widgets.Add(_landList.Root);
        }

        private void HandleRegionSelected(object sender, Region region)
        {
            Update();
        }

        private void HandleEmpireClick(object sender, System.EventArgs e)
        {
            var label = (Label)sender;
            _uiServiceProvider.EventManager.SelectEmpire(label.Id);
        }

        private void UpdateEmpire(Region region)
        {
            if (region.Empire != null)
            {
                _empireLabel.Id = region.Empire.Id;
                _empireLabel.Text = region.Empire.Name;
                _empireLabel.TouchDown += HandleEmpireClick;
            }
        }

        private void UpdateResources(Region region)
        {
            _resourceList.Widgets.Clear();

            foreach (var entry in region.Resources.Resources)
            {
                _resourceList.Widgets.Add(new Label
                {
                    Text = $"{entry.Key}: {entry.Value} (+{region.Resources.Production[entry.Key]} " +
                        $"| -{region.Resources.Upkeep[entry.Key]})"
                });
            }
        }

        private void UpdateBuildingQueue(Region region)
        {
            _buildingQueue.Widgets.Clear();

            foreach (var building in region.BuildingQueue)
            {
                _buildingQueue.Widgets.Add(new Label
                {
                    Text = $"{building.Name} ({building.RemainingTurns})"
                });
            }
        }

        public override void Update()
        {
            if (_uiServiceProvider.EventManager.SelectedRegion != null)
            {
                var region = _uiServiceProvider.EventManager.SelectedRegion;
                _regionNameLabel.Text = region.Name;
                _landList.Update();
                UpdateEmpire(region);
                UpdateBuildingQueue(region);
                UpdateResources(region);
            }
        }
    }
}

