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
            : base("../../../UI/InfoPanel/EmpireInfo/EmpireInfo.xaml", serviceProvider, eventManager, assetManager)
        {
            _empireNameLabel = (Label)Root.FindWidgetById("EmpireNameLabel");
            _eventManager.OnEmpireSelected += HandleEmpireSelected;

            _regionList = new EntityListController<Region>(
                serviceProvider.GlobalState.World.Regions,
                serviceProvider,
                eventManager,
                assetManager,
                ((region) => region.Empire == eventManager.SelectedEmpire)
            );
            _regionList.EntityClickHandler = eventManager.SelectRegion;

            var regionListPanel = (VerticalStackPanel)Root.FindWidgetById("RegionList");
            regionListPanel.Widgets.Add(_regionList.Root);
        }

        private void HandleEmpireSelected(object sender, Empire empire)
        {
            Update();
        }

        private void Clear()
        {
            _empireNameLabel.Text = "";
            _regionList.Clear();
        }

        public override void Update()
        {
            if (_eventManager.SelectedEmpire != null)
            {
                var empire = _eventManager.SelectedEmpire;
                _empireNameLabel.Text = empire.Name;
                _regionList.Update();
            }
        }
    }
}

