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
        private readonly VerticalStackPanel _resourceList;

        public EmpireInfoController(ServiceProvider serviceProvider, UIEventManager eventManager,AssetManager assetManager)
            : base("../../../UI/Sidebar/EmpireInfo/EmpireInfo.xaml", serviceProvider, eventManager, assetManager)
        {
            _empireNameLabel = (Label)Root.FindWidgetById("EmpireNameLabel");
            _resourceList = (VerticalStackPanel)Root.FindWidgetById("ResourceList");
            _eventManager.OnEmpireSelected += handleEmpireSelected;

            _regionList = new EntityListController<Region>(
                serviceProvider,
                eventManager,
                assetManager,
                ((region) => region.EmpireId == eventManager.SelectedEmpire.Id)
            );
            _regionList.EntityClickHandler = eventManager.SelectRegion;

            var regionListPanel = (VerticalStackPanel)Root.FindWidgetById("RegionList");
            regionListPanel.Widgets.Add(_regionList.Root);
        }

        private void handleEmpireSelected(object sender, Empire empire)
        {
            Update();
        }

        private void UpdateResources(Empire empire)
        {
            _resourceList.Widgets.Clear();

            foreach (var entry in empire.Resources)
            {
                _resourceList.Widgets.Add(new Label
                {
                    Text = $"{entry.Key}: {entry.Value} ({empire.Production[entry.Key]})"
                });
            }
        }

        public override void Update()
        {
            if (_eventManager.SelectedEmpire != null)
            {
                var empire = _eventManager.SelectedEmpire;
                _empireNameLabel.Text = empire.Name;
                _regionList.Update();
                UpdateResources(empire);
            }
        }
    }
}

