using System.IO;
using FSG.Core;
using FSG.Commands;
using FSG.Entities;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class RegionInfoController : UIController
    {
        private readonly Label _regionNameLabel;
        private readonly Label _empireLabel;
        private readonly EntityListController<Land> _landList;

        public RegionInfoController(ServiceProvider serviceProvider, UIEventManager eventManager, AssetManager assetManager)
            : base("../../../UI/Sidebar/RegionInfo/RegionInfo.xaml", serviceProvider, eventManager, assetManager)
        {
            _regionNameLabel = (Label)Root.FindWidgetById("RegionNameLabel");
            _empireLabel = (Label)Root.FindWidgetById("EmpireLabel");

            _eventManager.OnRegionSelected += HandleRegionSelected;

            _landList = new EntityListController<Land>(serviceProvider, eventManager, assetManager, ((land) => land.RegionId == eventManager.SelectedRegionId));
            _landList.EntityClickHandler = eventManager.SelectLand;

            var landListPanel = (VerticalStackPanel)Root.FindWidgetById("LandList");
            landListPanel.Widgets.Add(_landList.Root);
        }

        private void HandleRegionSelected(object sender, string regionId)
        {
            Update();
        }

        private void HandleEmpireClick(object sender, System.EventArgs e)
        {
            var label = (Label)sender;
            _eventManager.SelectEmpire(label.Id);
        }

        private void UpdateEmpire(Region region)
        {
            if (region.EmpireId != null)
            {
                var empire = _serviceProvider.GlobalState.Entities.Get<Empire>((EntityId<Empire>)region.EmpireId);
                _empireLabel.Id = empire.Id;
                _empireLabel.Text = empire.Name;
                _empireLabel.TouchDown += HandleEmpireClick;
            }
        }

        public override void Update(ICommand command = null)
        {
            if (_eventManager.SelectedRegionId != null)
            {
                var region = _serviceProvider.GlobalState.Entities.Get<Region>(new EntityId<Region>(_eventManager.SelectedRegionId));
                _regionNameLabel.Text = region.Name;
                _landList.Update(command);
                UpdateEmpire(region);
            }
        }
    }
}

