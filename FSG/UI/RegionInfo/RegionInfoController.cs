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

        public RegionInfoController(ServiceProvider serviceProvider, Desktop desktop, UIEventManager eventManager, AssetManager assetManager)
            : base("../../../UI/RegionInfo/RegionInfo.xaml", serviceProvider, desktop, eventManager, assetManager)
        {
            _regionNameLabel = (Label)Root.FindWidgetById("RegionNameLabel");
            _eventManager.OnRegionSelected += handleRegionSelected;
        }

        private void handleRegionSelected(object sender, string regionId)
        {
            var region = _serviceProvider.GlobalState.Entities.Get<Region>(new EntityId<Region>(regionId));
            _regionNameLabel.Text = region.Name;
        }

        public override void Update(ICommand command)
        {

        }
    }
}

