using System;
using FSG.Commands;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
	public class CapitalInfoController : UIController
	{
        private readonly HorizontalStackPanel _districtList;

        private readonly VerticalStackPanel _builtDistrictList;

        private readonly HorizontalStackPanel _buildingList;

        private readonly VerticalStackPanel _builtBuildingList;

        public CapitalInfoController(ServiceProvider serviceProvider, UIEventManager eventManager, AssetManager assetManager)
            : base("../../../UI/InfoPanel/CapitalInfo/CapitalInfo.xaml", serviceProvider, eventManager, assetManager)
		{
            //_districtList = (HorizontalStackPanel)Root.FindWidgetById("DistrictList");
            //_builtDistrictList = (VerticalStackPanel)Root.FindWidgetById("BuiltDistrictList");
            _buildingList = (HorizontalStackPanel)Root.FindWidgetById("BuildingList");
            _builtBuildingList = (VerticalStackPanel)Root.FindWidgetById("BuiltBuildingList");

            eventManager.OnRegionSelected += HandleRegionSelected;
		}

        private void HandleRegionSelected(object sender, Region e)
        {
            //UpdateDistricts();
            UpdateBuildings();
        }

        private void UpdateDistrictList()
        {
            _districtList.Widgets.Clear();

            var districts = _serviceProvider.Definitions.GetAll<BuildingDefinition>()
                .FindAll(definition => definition.BuildingType == BuildingType.District);

            foreach (var district in districts)
            {
                var districtBtn = new TextButton
                {
                    Id = district.Name,
                    Text = district.Name
                };
                _districtList.Widgets.Add(districtBtn);
                districtBtn.Click += HandleBuildDistrictClick;
            }
        }

        private void UpdateBuildingList()
        {
            _buildingList.Widgets.Clear();

            var buildings = _serviceProvider.Definitions.GetAll<BuildingDefinition>()
                .FindAll(definition => definition.BuildingType == BuildingType.CapitalBuilding);

            foreach (var building in buildings)
            {
                var buildingBtn = new TextButton
                {
                    Id = building.Name,
                    Text = building.Name
                };
                _buildingList.Widgets.Add(buildingBtn);
                buildingBtn.Click += HandleBuildBuildingClick;
            }
        }

        private void HandleBuildBuildingClick(object sender, EventArgs e)
        {
            var region = _eventManager.SelectedRegion;

            var buildingBtn = (TextButton)sender;

            _serviceProvider.Dispatcher.Dispatch(new AddBuildingToQueue
            {
                BuildingName = buildingBtn.Id,
                RegionId = region.Id,
                BuildingType = BuildingType.CapitalBuilding,
                EmpireId = region.Empire.Id,
            });
        }

        private void HandleBuildDistrictClick(object sender, EventArgs e)
        {
            var region = _eventManager.SelectedRegion;

            var districtBtn = (TextButton)sender;

            _serviceProvider.Dispatcher.Dispatch(new AddBuildingToQueue
            {
                BuildingName = districtBtn.Id,
                RegionId = region.Id,
                BuildingType = BuildingType.District,
                EmpireId = region.Empire.Id,
            });
        }

        private void UpdateBuiltDistrictList(Region region)
        {
            _builtDistrictList.Widgets.Clear();

            foreach (var district in region.Capital.Districts)
            {
                _builtDistrictList.Widgets.Add(new Label
                {
                    Text = district.Name
                });
            }
        }

        private void UpdateBuiltBuildingList(Region region)
        {
            _builtBuildingList.Widgets.Clear();

            foreach (var building in region.Capital.Buildings)
            {
                _builtBuildingList.Widgets.Add(new Label
                {
                    Text = building
                });
            }
        }

        private void UpdateDistricts()
        {
            var region = _eventManager.SelectedRegion;

            UpdateBuiltDistrictList(region);
            UpdateDistrictList();
        }

        private void UpdateBuildings()
        {
            var region = _eventManager.SelectedRegion;

            UpdateBuiltBuildingList(region);
            UpdateBuildingList();
        }

        public override void Update()
        {
            if (_eventManager.SelectedRegion != null)
            {
                //UpdateDistricts();
                UpdateBuildings();
            }
        }
    }
}

