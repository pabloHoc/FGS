using System;
using FSG.Commands;
using FSG.Definitions;
using FSG.Entities;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
	public class CapitalInfoController : UIController
	{
        private readonly HorizontalStackPanel _districtList;

        private readonly VerticalStackPanel _builtDistrictList;

        private readonly HorizontalStackPanel _buildingList;

        private readonly VerticalStackPanel _builtBuildingList;

        public CapitalInfoController(UIServiceProvider uiServiceProvider)
            : base("../../../UI/InfoPanel/CapitalInfo/CapitalInfo.xaml", uiServiceProvider)
		{
            //_districtList = (HorizontalStackPanel)Root.FindWidgetById("DistrictList");
            //_builtDistrictList = (VerticalStackPanel)Root.FindWidgetById("BuiltDistrictList");
            _buildingList = (HorizontalStackPanel)Root.FindWidgetById("BuildingList");
            _builtBuildingList = (VerticalStackPanel)Root.FindWidgetById("BuiltBuildingList");

            uiServiceProvider.EventManager.OnRegionSelected += HandleRegionSelected;
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
            var region = _uiServiceProvider.EventManager.SelectedRegion;

            var buildingBtn = (TextButton)sender;

            _serviceProvider.Dispatcher.Dispatch(new QueueBuilding
            {
                BuildingName = buildingBtn.Id,
                RegionId = region.Id,
                BuildingType = BuildingType.CapitalBuilding,
                EmpireId = region.Empire.Id,
            });
        }

        private void HandleBuildDistrictClick(object sender, EventArgs e)
        {
            var region = _uiServiceProvider.EventManager.SelectedRegion;

            var districtBtn = (TextButton)sender;

            _serviceProvider.Dispatcher.Dispatch(new QueueBuilding
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
            var region = _uiServiceProvider.EventManager.SelectedRegion;

            UpdateBuiltDistrictList(region);
            UpdateDistrictList();
        }

        private void UpdateBuildings()
        {
            var region = _uiServiceProvider.EventManager.SelectedRegion;

            UpdateBuiltBuildingList(region);
            UpdateBuildingList();
        }

        public override void Update()
        {
            if (_uiServiceProvider.EventManager.SelectedRegion != null)
            {
                //UpdateDistricts();
                UpdateBuildings();
            }
        }
    }
}

