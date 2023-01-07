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
        private readonly VerticalStackPanel _buildingQueue;

        public LandInfoController(
            ServiceProvider serviceProvider,
            UIEventManager eventManager,
            AssetManager assetManager
        ) : base(
            "../../../UI/Sidebar/LandInfo/LandInfo.xaml",
            serviceProvider,
            eventManager,
            assetManager
        )
        {
            _landNameLabel = (Label)Root.FindWidgetById("LandNameLabel");
            _regionLabel = (Label)Root.FindWidgetById("RegionLabel");
            _builtBuildingList = (VerticalStackPanel)Root.FindWidgetById("BuiltBuildingList");
            _buildingList = (HorizontalStackPanel)Root.FindWidgetById("BuildingList");
            _buildingQueue = (VerticalStackPanel)Root.FindWidgetById("BuildingQueue");
            _eventManager.OnLandSelected += HandleLandSelected;
        }

        private void HandleLandSelected(object sender, string landId)
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
            _landNameLabel.Text = land.Name;

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

            var buildingDefinitions = _serviceProvider.Definitions
                .GetAll<BuildingDefinition>();

            var empire = _serviceProvider.GlobalState.Entities
                .Get(new EntityId<Empire>(_eventManager.SelectedEmpireId));

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

        private void UpdateBuildingQueue(Land land)
        {
            _buildingQueue.Widgets.Clear();

            foreach(var building in land.BuildingQueue)
            {
                _buildingQueue.Widgets.Add(new Label
                {
                    Text = $"{building.Name} ({building.RemainingTurns})"
                });
            }
        }

        private void UpdateRegion(Land land)
        {
            var region = _serviceProvider.GlobalState.Entities.Get<Region>(land.RegionId);
            _regionLabel.Id = region.Id;
            _regionLabel.Text = region.Name;
            _regionLabel.TouchDown += HandleRegionClick;
        }

        private void HandleBuildingClick(object sender, System.EventArgs e)
        {
            _serviceProvider.Dispatcher.Dispatch(new AddBuildingtoQueue
            {
                BuildingName = ((TextButton)sender).Id,
                LandId = new EntityId<Land>(_eventManager.SelectedLandId),
                EmpireId = new EntityId<Empire>(_eventManager.SelectedEmpireId)
            });
        }

        public override void Update(ICommand command = null)
        {
            if (_eventManager.SelectedLandId != null)
            {
                var land = _serviceProvider.GlobalState.Entities
                    .Get<Land>(new EntityId<Land>(_eventManager.SelectedLandId));

                UpdateBuiltBuildingList(land);
                UpdateBuildingList(land);
                UpdateBuildingQueue(land);
                UpdateRegion(land);
            }
        }
    }
}

