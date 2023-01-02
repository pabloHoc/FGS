using System.IO;
using FSG.Core;
using FSG.Commands;
using FSG.Entities;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class LandInfoController : UIController
    {
        private readonly Label _landNameLabel;

        public LandInfoController(ServiceProvider serviceProvider, UIEventManager eventManager, AssetManager assetManager)
            : base("../../../UI/Sidebar/LandInfo/LandInfo.xaml", serviceProvider, eventManager, assetManager)
        {
            _landNameLabel = (Label)Root.FindWidgetById("LandNameLabel");
            _eventManager.OnLandSelected += handleLandSelected;
        }

        private void handleLandSelected(object sender, string landId)
        {
            var land = _serviceProvider.GlobalState.Entities.Get<Land>(new EntityId<Land>(landId));
            _landNameLabel.Text = land.Name;
        }

    }
}

