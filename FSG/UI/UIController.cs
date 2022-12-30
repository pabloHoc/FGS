using System.IO;
using FSG.Core;
using FSG.Commands;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public abstract class UIController
    {
        protected readonly ServiceProvider _serviceProvider;
        protected readonly Desktop _desktop;
        protected readonly Widget _root;

        public UIController(string xmlPath, ServiceProvider serviceProvider, Desktop desktop, AssetManager assetManager)
        {
            _serviceProvider = serviceProvider;
            _desktop = desktop;

            string data = File.ReadAllText(xmlPath);
            Project project = Project.LoadFromXml(data, assetManager);
            _root = project.Root;

            Show();
        }

        public void Show()
        {
            _desktop.Widgets.Add(_root);
        }

        public void Hide()
        {
            _desktop.Widgets.Remove(_root);
        }

        public abstract void Update(ICommand command);
    }
}

