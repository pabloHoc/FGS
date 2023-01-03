using System.IO;
using FSG.Core;
using FSG.Commands;
using FSG.Entities;
using Myra.Assets;
using Myra.Graphics2D.UI;
using System;

namespace FSG.UI
{
    public class EmpireInfoController : UIController
    {
        private readonly Label _empireNameLabel;
        private readonly EntityListController<Region> _regionList;

        public EmpireInfoController(ServiceProvider serviceProvider, UIEventManager eventManager,AssetManager assetManager)
            : base("../../../UI/Sidebar/EmpireInfo/EmpireInfo.xaml", serviceProvider, eventManager, assetManager)
        {
            _empireNameLabel = (Label)Root.FindWidgetById("EmpireNameLabel");
            _eventManager.OnEmpireSelected += handleEmpireSelected;

            _regionList = new EntityListController<Region>(
                serviceProvider,
                eventManager,
                assetManager,
                ((region) => region.EmpireId.HasValue && region.EmpireId?.Value == eventManager.SelectedEmpireId)
            );
            _regionList.EntityClickHandler = eventManager.SelectRegion;

            var regionListPanel = (VerticalStackPanel)Root.FindWidgetById("RegionList");
            regionListPanel.Widgets.Add(_regionList.Root);
        }

        private void handleEmpireSelected(object sender, string empireId)
        {
            Update();
        }

        public override void Update(ICommand command = null)
        {
            if (_eventManager.SelectedEmpireId != null)
            {
                var empire = _serviceProvider.GlobalState.Entities.Get<Empire>(new EntityId<Empire>(_eventManager.SelectedEmpireId));
                _empireNameLabel.Text = empire.Name;
                _regionList.Update(command);
            }
        }
    }
}

