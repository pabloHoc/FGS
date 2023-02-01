using System.IO;
using FSG.Core;
using FSG.Commands;
using FSG.Definitions;
using FSG.Entities;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class LandInfoController : UIController
    {
        private readonly Label _landNameLabel;

        private readonly Label _regionLabel;

        private readonly VerticalStackPanel _builtBuildingList;

        private readonly HorizontalStackPanel _buildingList;

        public LandInfoController(
            ServiceProvider serviceProvider,
            UIEventManager eventManager,
            AssetManager assetManager
        ) : base(
            "../../../UI/InfoPanel/LandInfo/LandInfo.xaml",
            serviceProvider,
            eventManager,
            assetManager
        )
        {
            _landNameLabel = (Label)Root.FindWidgetById("LandNameLabel");
            _regionLabel = (Label)Root.FindWidgetById("RegionLabel");
            _builtBuildingList = (VerticalStackPanel)Root.FindWidgetById("BuiltBuildingList");
            _buildingList = (HorizontalStackPanel)Root.FindWidgetById("BuildingList");
            _eventManager.OnLandSelected += HandleLandSelected;
        }

        private void HandleLandSelected(object sender, Land land)
        {
            Update();
        }

        private void HandleRegionClick(object sender, System.EventArgs e)
        {
            var label = (Label)sender;
            _eventManager.SelectRegion(label.Id);
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
            var empire = _eventManager.SelectedEmpire;

            foreach(var building in buildingDefinitions)
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
                LandId = _eventManager.SelectedLand.Id,
                RegionId = _eventManager.SelectedRegion.Id,
                EmpireId = _eventManager.SelectedEmpire.Id
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
            if (_eventManager.SelectedLand != null && _eventManager.SelectedEmpire != null)
            {
                var land = _eventManager.SelectedLand;
                _landNameLabel.Text = land.Name;

                UpdateBuiltBuildingList(land);
                UpdateBuildingList(land);
                UpdateRegion(land);
            } else
            {
                Clear();
            }
        }
    }
}

