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

        public TopbarController(ServiceProvider serviceProvider, UIEventManager eventManager, AssetManager assetManager)
            : base("../../../UI/Topbar/Topbar.xaml", serviceProvider, eventManager, assetManager)
        {
            _empireNameLabel = (Label)Root.FindWidgetById("EmpireNameLabel");
            _resourceList = (HorizontalStackPanel)Root.FindWidgetById("ResourceList");

            _eventManager.OnEmpireSelected += HandleEmpireSelected;
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
                    Text = $"{entry.Key}: {entry.Value} (+{empire.Resources.Production[entry.Key]} " +
                        $"| -{empire.Resources.Upkeep[entry.Key]})",
                });
            }
        }

        private void Clear()
        {
            _empireNameLabel.Text = "";
            _resourceList.Widgets.Clear();
        }

        public override void Update()
        {
            if (_eventManager.SelectedEmpire != null)
            {
                var empire = _eventManager.SelectedEmpire;
                _empireNameLabel.Text = empire.Name;
                UpdateResources(empire);
            }
        }
    }
}

