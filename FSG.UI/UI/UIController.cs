using System.IO;
using FSG.Core;
using FSG.Commands;
using Myra.Assets;
using Myra.Graphics2D.UI;
using System.Collections.ObjectModel;

namespace FSG.UI
{
    public interface IContainerWidget
    {
        ObservableCollection<Widget> Widgets { get; }
    }

    public abstract class UIController
    {
        protected readonly UI _ui;
        protected readonly ServiceProvider _serviceProvider;
        protected readonly UIEventManager _eventManager;
        public Widget Root { get; init; }

        public UIController(
            string xmlPath,
            ServiceProvider serviceProvider,
            UIEventManager eventManager,
            AssetManager assetManager)
        {
            _serviceProvider = serviceProvider;
            _eventManager = eventManager;

            string data = File.ReadAllText(xmlPath);
            Project project = Project.LoadFromXml(data, assetManager);
            Root = project.Root;
        }

        public virtual void Update(ICommand command) { }
    }
}

