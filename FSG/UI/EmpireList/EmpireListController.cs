using System.IO;
using FSG.Core;
using FSG.Commands;
using FSG.Entities;
using Myra.Assets;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework;

namespace FSG.UI
{
    public class EmpireListController : UIController
    {
        private readonly VerticalStackPanel _panel;
        private readonly Grid _list;
        private readonly ScrollViewer _content;
        private readonly TextButton _toggleBtn;
        private bool _show = true;

        public EmpireListController(ServiceProvider serviceProvider, Desktop desktop, AssetManager assetManager)
            : base("../../../UI/EmpireList/EmpireList.xaml", serviceProvider, desktop, assetManager)
        {
            _panel = (VerticalStackPanel)_root.FindWidgetById("EmpireListPanel");
            _list = (Grid)_root.FindWidgetById("EmpireListGrid");
            _content = (ScrollViewer)_root.FindWidgetById("EmpireListScrollViewer");
            _toggleBtn = (TextButton)_root.FindWidgetById("EmpireListToggleBtn");
            _toggleBtn.Click += handleToggle;

            GenerateEmpireList();
        }

        private void GenerateEmpireList()
        {
            var empires = _serviceProvider.GlobalState.Entities.GetAll<Empire>();
            var count = 1;

            _list.Widgets.Clear();

            foreach (var empire in empires)
            {
                var empireLabel = new Label();
                empireLabel.Text = empire.Name;
                empireLabel.GridRow = count;
                _list.AddChild(empireLabel);
                count++;
            }
        }

        private void handleToggle(object sender, System.EventArgs e)
        {
            if (_show)
            {
                _panel.Widgets.Remove(_content);
                _show = false;
            }
            else
            {
                _panel.Widgets.Add(_content);
                _show = true;
            }
        }

        public override void Update(ICommand command)
        {
            GenerateEmpireList();
        }
    }
}

