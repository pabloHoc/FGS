using FSG.Commands;
using FSG.Definitions;
using FSG.Entities;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class LandInfoController : UIController
    {
        private readonly Label _landNameLabel;

        private readonly Label _regionLabel;

        private readonly VerticalStackPanel _builtBuildingList;

        private readonly HorizontalStackPanel _buildingList;

        public LandInfoController(UIServiceProvider uiServiceProvider) :
            base(
            "../../../UI/InfoPanel/LandInfo/LandInfo.xaml",
            uiServiceProvider
        )
        {
            _landNameLabel = (Label)Root.FindWidgetById("LandNameLabel");
            _regionLabel = (Label)Root.FindWidgetById("RegionLabel");
            _builtBuildingList = (VerticalStackPanel)Root.FindWidgetById("BuiltBuildingList");
            _buildingList = (HorizontalStackPanel)Root.FindWidgetById("BuildingList");
            _uiServiceProvider.EventManager.OnLandSelected += HandleLandSelected;
        }

        private void HandleLandSelected(object sender, Land land)
        {
            Update();
        }

        private void HandleRegionClick(object sender, System.EventArgs e)
        {
            var label = (Label)sender;
            _uiServiceProvider.EventManager.SelectRegion(label.Id);
        }

        private void UpdateBuiltBuildingList(Land land)
        {
            _builtBuildingList.Widgets.Clear();

            foreach (var building in land.Buildings)
            {
                _builtBuildingList.Widgets.Add(new Label
                {
                    Text = building
                });
            }
        }

        private void UpdateBuildingList(Land land)
        {
            _buildingList.Widgets.Clear();

            var buildingDefinitions = _serviceProvider.Definitions.GetAll<BuildingDefinition>()
                .FindAll(definition => definition.BuildingType == BuildingType.LandBuilding);

            // TODO: check there's an empire selected
            var empire = _uiServiceProvider.EventManager.SelectedEmpire;

            foreach (var building in buildingDefinitions)
            {
                var buildingBtn = new TextButton
                {
                    Id = building.Name,
                    Text = building.Name,
                    Enabled = _serviceProvider.Services.BuildingService.Allow(empire, land, building)
                };
                buildingBtn.Click += HandleBuildingClick;
                _buildingList.Widgets.Add(buildingBtn);
            }
        }

        private void UpdateRegion(Land land)
        {
            _regionLabel.Id = land.Region.Id;
            _regionLabel.Text = land.Region.Name;
            _regionLabel.TouchDown += HandleRegionClick;
        }

        private void HandleBuildingClick(object sender, System.EventArgs e)
        {
            _serviceProvider.Dispatcher.Dispatch(new AddBuildingToQueue
            {
                BuildingName = ((TextButton)sender).Id,
                BuildingType = BuildingType.LandBuilding,
                LandId = _uiServiceProvider.EventManager.SelectedLand.Id,
                RegionId = _uiServiceProvider.EventManager.SelectedRegion.Id,
                EmpireId = _uiServiceProvider.EventManager.SelectedEmpire.Id
            });
        }

        public void Clear()
        {
            _landNameLabel.Text = "";
            _buildingList.Widgets.Clear();
            _builtBuildingList.Widgets.Clear();
        }

        public override void Update()
        {
            if (_uiServiceProvider.EventManager.SelectedLand != null && _uiServiceProvider.EventManager.SelectedEmpire != null)
            {
                var land = _uiServiceProvider.EventManager.SelectedLand;
                _landNameLabel.Text = land.Name;

                UpdateBuiltBuildingList(land);
                UpdateBuildingList(land);
                UpdateRegion(land);
            }
            else
            {
                Clear();
            }
        }
    }
}

