using System.IO;
using FSG.Core;
using FSG.Commands;
using FSG.Entities;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class EmpireInfoController : UIController
    {
        private readonly Label _empireNameLabel;

        public EmpireInfoController(ServiceProvider serviceProvider, Desktop desktop, UIEventManager _eventManager,AssetManager assetManager)
            : base("../../../UI/EmpireInfo/EmpireInfo.xaml", serviceProvider, desktop, _eventManager, assetManager)
        {
            _empireNameLabel = (Label)Root.FindWidgetById("EmpireNameLabel");
            _eventManager.OnEmpireSelected += handleEmpireSelected;
        }

        private void handleEmpireSelected(object sender, string empireId)
        {
            var empire = _serviceProvider.GlobalState.Entities.Get<Empire>(new EntityId<Empire>(empireId));
            _empireNameLabel.Text = empire.Name;
        }

        public override void Update(ICommand command)
        {

        }
    }
}

