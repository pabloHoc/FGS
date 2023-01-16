using System;
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

        private void HandleRegionSelected(object sender, string e)
        {
            UpdateDistricts();
        }

        private void UpdateDistrictList()
        {
            _districtList.Widgets.Clear();

            var districts = _serviceProvider.Definitions.GetAll<DistrictDefinition>();

            foreach (var district in districts)
            {
                _districtList.Widgets.Add(new TextButton
                {
                    Text = district.Name
                });
            }
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
            var region = _serviceProvider.GlobalState.Entities
                .Get<Region>(new EntityId<Region>(_eventManager.SelectedRegionId));

            UpdateBuiltDistrictList(region);
            UpdateDistrictList();
        }

        public void Update()
        {
            if (_eventManager.SelectedRegionId != null)
            {
                UpdateDistricts();
            }
        }
    }
}

