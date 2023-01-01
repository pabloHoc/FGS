using System.IO;
using FSG.Core;
using FSG.Commands;
using FSG.Entities;
using Myra.Assets;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace FSG.UI
{
    public class EntityListController<T> : UIController where T : IEntity<T>, INameableEntity
    {
        private readonly Grid _list;
        public Action<string> EntityClickHandler { get; set; } 

        public EntityListController(ServiceProvider serviceProvider, Desktop desktop, UIEventManager eventManager, AssetManager assetManager)
            : base("../../../UI/EntityList/EntityList.xaml", serviceProvider, desktop, eventManager, assetManager)
        {
            _list = (Grid)Root.FindWidgetById("EntityListGrid");

            GenerateEntityList();
        }

        private void GenerateEntityList()
        {
            var entities = _serviceProvider.GlobalState.Entities.GetAll<T>();
            var count = 1;

            _list.Widgets.Clear();

            foreach (var entity in entities)
            {
                var empireLabel = new Label();
                empireLabel.Id = entity.Id.Value;
                empireLabel.TouchDown += handleEntityClick;
                empireLabel.Text = ((INameableEntity)entity).Name;
                empireLabel.GridRow = count;
                _list.AddChild(empireLabel);
                count++;
            }
        }

        private void handleEntityClick(object empireLabel, System.EventArgs e)
        {
            var entityId = ((Label)empireLabel).Id;
            EntityClickHandler(entityId);
        }

        public override void Update(ICommand command)
        {
            GenerateEntityList();
        }

        public override void Show()
        {

        }

        public override void Hide()
        {

        }
    }
}

