using FSG.Entities;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class EmpireInfoController : UIController
    {
        private readonly Label _empireNameLabel;

        private readonly EntityListController<Region> _regionList;

        public EmpireInfoController(UIServiceProvider uiServiceProvider)
            : base("../../../UI/InfoPanel/EmpireInfo/EmpireInfo.xaml", uiServiceProvider)
        {
            _empireNameLabel = (Label)Root.FindWidgetById("EmpireNameLabel");
            _uiServiceProvider.EventManager.OnEmpireSelected += HandleEmpireSelected;

            _regionList = new EntityListController<Region>(
                _serviceProvider.GlobalState.World.Regions,
                _uiServiceProvider,
                ((region) => region.Empire == _uiServiceProvider.EventManager.SelectedEmpire)
            )
            {
                EntityClickHandler = _uiServiceProvider.EventManager.SelectRegion
            };

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
            if (_uiServiceProvider.EventManager.SelectedEmpire != null)
            {
                var empire = _uiServiceProvider.EventManager.SelectedEmpire;
                _empireNameLabel.Text = empire.Name;
                _regionList.Update();
            }
            else
            {
                Clear();
            }
        }
    }
}

