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

        protected readonly UIServiceProvider _uiServiceProvider;

        protected readonly ServiceProvider _serviceProvider;

        public Widget Root { get; init; }

        public UIController(
            string xmlPath,
            UIServiceProvider uiServiceProvider
            )
        {
            _uiServiceProvider = uiServiceProvider;
            _serviceProvider = uiServiceProvider.GameServiceProvider;

            string data = File.ReadAllText(xmlPath);
            Project project = Project.LoadFromXml(data, uiServiceProvider.AssetManager);
            Root = project.Root;
        }

        public virtual void Update() { }

        public virtual void Update(ICommand command) { }
    }
}

