﻿using System.IO;
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
    public class EntityListController<T> : UIController where T : IEntity<T>, INameable
    {
        private readonly Grid _list;

        private readonly Predicate<T> _predicate;

        public Action<string> EntityClickHandler { get; set; }

        private readonly List<T> _entities;

        public EntityListController(
            List<T> entities,
            ServiceProvider serviceProvider,
            UIEventManager eventManager,
            AssetManager assetManager,
            Predicate<T> predicate = null
        ) : base("../../../UI/Common/EntityList/EntityList.xaml", serviceProvider, eventManager, assetManager)
        {
            _entities = entities;
            _list = (Grid)Root.FindWidgetById("EntityListGrid");
            _predicate = predicate;
        }

        private void GenerateEntityList()
        {
            var count = 1;

            _list.Widgets.Clear();

            var entities = _predicate != null ? _entities.FindAll(_predicate) : _entities;

            foreach (var entity in entities)
            {
                var entityLabel = new Label();
                entityLabel.Id = entity.Id;
                entityLabel.TouchDown += HandleEntityClick;
                entityLabel.Text = ((INameable)entity).Name;
                entityLabel.GridRow = count;
                _list.AddChild(entityLabel);
                count++;
            }
        }

        private void HandleEntityClick(object entityLabel, System.EventArgs e)
        {
            if (EntityClickHandler != null)
            {
                var entityId = ((Label)entityLabel).Id;
                EntityClickHandler(entityId);
            }
        }

        public void Clear()
        {
            _list.Widgets.Clear();
        }

        public override void Update()
        {
            GenerateEntityList();
        }
    }
}

