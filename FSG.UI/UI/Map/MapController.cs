using System;
using System.Reflection.Metadata;
using FSG.Core;
using FSG.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D;

namespace FSG.UI
{
    public class MapController
    {
        private readonly ServiceProvider _serviceProvider;

        private readonly UIEventManager _eventManager;

        private readonly GraphicsDevice _graphicsDevice;

        private readonly SpriteBatch _spriteBatch;

        private readonly Texture2D _texture;

        public MapController(
            ServiceProvider serviceProvider,
            UIEventManager eventManager,
            GraphicsDevice graphicsDevice,
            SpriteBatch spriteBatch
        )
        {
            _serviceProvider = serviceProvider;
            _eventManager = eventManager;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = spriteBatch;

            _texture = new Texture2D(_graphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.White });
        }

        private void DrawGrid()
        {
            var COLUMNS = 10;
            var ROWS = 10;
            var CHUNK_SIZE = 100;

            for (int i = 0; i < ROWS; i++)
            {
                var rectangle = new Rectangle(0, i * CHUNK_SIZE, ROWS * CHUNK_SIZE, 1);
                _spriteBatch.Draw(_texture, rectangle, Color.Black);
            }

            for (int i = 0; i < COLUMNS; i++)
            {
                var rectangle = new Rectangle(i * CHUNK_SIZE, 0, 1, COLUMNS * CHUNK_SIZE);
                _spriteBatch.Draw(_texture, rectangle, Color.Black);
            }
        }

        private void DrawRegion(Region region)
        {
            var REGION_SIZE = 20;
            var REGION_COLOR = Color.Green;

            var rectangle = new Rectangle(region.X - REGION_SIZE / 2, region.Y - REGION_SIZE / 2, REGION_SIZE, REGION_SIZE);
            _spriteBatch.Draw(_texture, rectangle, REGION_COLOR);
        }

        private void DrawRoads(Region region)
        {
            foreach (var connectedRegionId in region.ConnectedTo)
            {
                var connectedRegion = _serviceProvider.GlobalState.Entities.Get(connectedRegionId);

                // TODO: Conver to extension method
                var start = new Vector2(region.X, region.Y);
                var end = new Vector2(connectedRegion.X, connectedRegion.Y);

                _spriteBatch.Draw(
                    _texture,
                    start,
                    null,
                    Color.Black,
                    (float)Math.Atan2(end.Y - start.Y, end.X - start.X),
                    new Vector2(0f, (float)_texture.Height / 2),
                    new Vector2(Vector2.Distance(start, end), 1f),
                    SpriteEffects.None,
                    0f
                );
            }
        }

        public void Update()
        {
            DrawGrid();

            var regions = _serviceProvider.GlobalState.Entities.GetAll<Region>();

            foreach (var region in regions)
            {
                DrawRegion(region);
                DrawRoads(region);
            }
        }
    }
}

