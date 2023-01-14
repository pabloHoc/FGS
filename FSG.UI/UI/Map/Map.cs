using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using FSG.Commands;
using FSG.Core;
using FSG.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using MonoGame.Extended.Timers;
using Myra.Graphics2D;

namespace FSG.UI
{
    public class Map
    {
        private readonly ServiceProvider _serviceProvider;

        private readonly UIEventManager _eventManager;

        private readonly GraphicsDevice _graphicsDevice;

        private readonly SpriteBatch _spriteBatch;

        private readonly OrthographicCamera _camera;

        private readonly Texture2D _texture;

        private readonly List<MapLocation> _locations = new List<MapLocation>();

        private int _currentMouseWheelValue = 0;

        public Map(
            ServiceProvider serviceProvider,
            UIEventManager eventManager,
            GraphicsDevice graphicsDevice,
            SpriteBatch spriteBatch,
            OrthographicCamera camera
        )
        {
            _serviceProvider = serviceProvider;
            _eventManager = eventManager;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = spriteBatch;
            _camera = camera;
            _texture = new Texture2D(_graphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.White });

            _eventManager.OnRegionSecondaryAction += HandleRegionRightClick;
        }

        private void HandleRegionRightClick(object sender, string e)
        {
            if (_eventManager.SelectedAgentId != null)
            {
                _serviceProvider.Dispatcher.Dispatch(new SetEntityCurrentAction<Agent>
                {
                    EntityId = new EntityId<Agent>(_eventManager.SelectedAgentId),
                    EntityType = EntityType.Agent,
                    NewCurrentAction = new ActionQueueItem
                    {
                        ActionType = ActionType.Action,
                        Name = "Move",
                        RemainingTurns = 1,
                        Payload = e
                    }
                });
            }
        }

        public void Initialize()
        {
            var regions = _serviceProvider.GlobalState.Entities.GetAll<Region>();

            foreach (var region in regions)
            {
                _locations.Add(new MapLocation(region, _eventManager, _graphicsDevice, _spriteBatch, _camera));
            }

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

        public void Draw()
        {
            //DrawGrid();

            var regions = _serviceProvider.GlobalState.Entities.GetAll<Region>();

            foreach (var region in regions)
            {
                DrawRoads(region);
            }

            foreach (var location in _locations)
            {
                location.Draw();
            }
        }

        public void Update(GameTime gameTime)
        {
            var mouseState = MouseExtended.GetState();

            if (mouseState.X > 400)
            {
                UpdateCamera(gameTime);

                foreach (var location in _locations)
                {
                    location.Update();
                }   
            } else
            {
                _currentMouseWheelValue = mouseState.ScrollWheelValue;
            }
        }

        private void UpdateCamera(GameTime gameTime)
        {
            // Movement
            const float movementSpeed = 200;
            _camera.Move(GetCameraMovementDirection() * movementSpeed * gameTime.GetElapsedSeconds());

            // Zoom
            var mouseState = MouseExtended.GetState();

            var previousMouseWheelValue = _currentMouseWheelValue;
            _currentMouseWheelValue = MouseExtended.GetState().ScrollWheelValue;

            if (_currentMouseWheelValue > previousMouseWheelValue)
            {
                _camera.ZoomIn(1 / 12f);
            }

            if (_currentMouseWheelValue < previousMouseWheelValue)
            {
                _camera.ZoomOut(1 / 12f);
            }
        }

        private static Vector2 GetCameraMovementDirection()
        {
            var movementDirection = Vector2.Zero;
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Down))
            {
                movementDirection += Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                movementDirection -= Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Left))
            {
                movementDirection -= Vector2.UnitX;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                movementDirection += Vector2.UnitX;
            }
            return movementDirection;
        }
    }
}

