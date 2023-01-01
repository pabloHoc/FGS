using System.IO;
using FSG.Core;
using FSG.Commands;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public abstract class UIController
    {
        protected readonly UI _ui;
        protected readonly ServiceProvider _serviceProvider;
        protected readonly Desktop _desktop;
        protected readonly UIEventManager _eventManager;
        public Widget Root { get; init; }

        public UIController(
            string xmlPath,
            ServiceProvider serviceProvider,
            Desktop desktop,
            UIEventManager eventManager,
            AssetManager assetManager)
        {
            _serviceProvider = serviceProvider;
            _desktop = desktop;
            _eventManager = eventManager;

            string data = File.ReadAllText(xmlPath);
            Project project = Project.LoadFromXml(data, assetManager);
            Root = project.Root;

            Show();
        }

        public virtual void Show()
        {
            _desktop.Widgets.Add(Root);
        }

        public virtual void Hide()
        {
            _desktop.Widgets.Remove(Root);
        }

        public abstract void Update(ICommand command);
    }
}

