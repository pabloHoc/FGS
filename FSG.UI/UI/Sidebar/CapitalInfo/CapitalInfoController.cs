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

        public CapitalInfoController(ServiceProvider serviceProvider, UIEventManager eventManager, AssetManager assetManager)
            : base("../../../UI/Sidebar/CapitalInfo/CapitalInfo.xaml", serviceProvider, eventManager, assetManager)
		{
            _districtList = (HorizontalStackPanel)Root.FindWidgetById("DistrictList");
            _builtDistrictList = (VerticalStackPanel)Root.FindWidgetById("BuiltDistrictList");

            eventManager.OnRegionSelected += HandleRegionSelected;
		}

        private void HandleRegionSelected(object sender, Region e)
        {
            UpdateDistricts();
        }

        private void UpdateDistrictList()
        {
            _districtList.Widgets.Clear();

            var districts = _serviceProvider.Definitions.GetAll<DistrictDefinition>();

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

        private void HandleBuildDistrictClick(object sender, EventArgs e)
        {
            var region = _eventManager.SelectedRegion;

            var districtBtn = (TextButton)sender;

            _serviceProvider.Dispatcher.Dispatch(new AddBuildingToQueue
            {
                BuildingName = districtBtn.Id,
                RegionId = region.Id,
                BuildingType = BuildingType.District,
                EmpireId = (EntityId<Empire>)region.EmpireId,
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

        private void UpdateDistricts()
        {
            var region = _eventManager.SelectedRegion;

            UpdateBuiltDistrictList(region);
            UpdateDistrictList();
        }

        public override void Update()
        {
            if (_eventManager.SelectedRegion != null)
            {
                UpdateDistricts();
            }
        }
    }
}

