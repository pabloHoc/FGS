using System;
using FSG.Core;
using FSG.Entities;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
	public class TopbarController : UIController
    {
        private readonly Label _empireNameLabel;

        private readonly HorizontalStackPanel _resourceList;

        public TopbarController(UIServiceProvider uiServiceProvider)
            : base("../../../UI/Topbar/Topbar.xaml", uiServiceProvider)
        {
            _empireNameLabel = (Label)Root.FindWidgetById("EmpireNameLabel");
            _resourceList = (HorizontalStackPanel)Root.FindWidgetById("ResourceList");

            _uiServiceProvider.EventManager.OnRegionSelected += HandleRegionSelected;
            _uiServiceProvider.EventManager.OnEmpireSelected += HandleEmpireSelected;
        }

        private void HandleRegionSelected(object sender, Region e)
        {
            Update();
        }

        private void HandleEmpireSelected(object sender, Empire e)
        {
            Update();
        }

        private void UpdateResources(Empire empire)
        {
            _resourceList.Widgets.Clear();

            foreach (var entry in empire.Resources.Resources)
            {
                _resourceList.Widgets.Add(new Label
                {
                    Text = $"{entry.Key}: {entry.Value} (+{empire.Resources.Production[entry.Key]}" +
                        $",-{empire.Resources.Upkeep[entry.Key]})",
                });
                _resourceList.Widgets.Add(new VerticalSeparator());
            }
        }

        private void Clear()
        {
            _empireNameLabel.Text = "";
            _resourceList.Widgets.Clear();
        }

        public override void Update()
        {
            if (_uiServiceProvider.EventManager.SelectedEmpire != null)
            {
                var empire = _uiServiceProvider.EventManager.SelectedEmpire;
                _empireNameLabel.Text = empire.Name;
                UpdateResources(empire);
            }
            else
            {
                Clear();
            }
        }
    }
}

