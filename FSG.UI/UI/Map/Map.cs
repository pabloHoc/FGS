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
        struct Road
        {
            internal Vector2 Start { get; init; }

            internal Vector2 End { get; init; }
        }

        private readonly ServiceProvider _serviceProvider;

        private readonly UIEventManager _eventManager;

        private readonly GraphicsDevice _graphicsDevice;

        private readonly SpriteBatch _spriteBatch;

        private readonly OrthographicCamera _camera;

        private readonly Texture2D _texture;

        private readonly List<MapLocation> _locations = new List<MapLocation>();

        private int _currentMouseWheelValue = 0;

        private readonly List<Road> _roads = new List<Road>();

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

        private void HandleRegionRightClick(object sender, Region e)
        {
            if (_eventManager.SelectedAgent != null)
            {
                _serviceProvider.Dispatcher.Dispatch(new SetEntityCurrentAction<Agent>
                {
                    EntityId = _eventManager.SelectedAgent.Id,
                    EntityType = EntityType.Agent,
                    NewCurrentAction = new ActionQueueItem
                    {
                        ActionType = ActionType.Action,
                        Name = "Move",
                        RemainingTurns = 1,
                        Payload = e.Id
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
                GenerateRoads(region);
            }
        }

        private void GenerateRoads(Region region)
        {
            foreach (var connectedRegionId in region.ConnectedTo)
            {
                var connectedRegion = _serviceProvider.GlobalState.Entities.Get(connectedRegionId);
                _roads.Add(new Road
                {
                    Start = new Vector2(region.X, region.Y),
                    End = new Vector2(connectedRegion.X, connectedRegion.Y),
                });
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

        private void DrawRoads()
        {
            foreach (var road in _roads)
            {
                _spriteBatch.Draw(
                    _texture,
                    road.Start,
                    null,
                    Color.Black,
                    (float)Math.Atan2(road.End.Y - road.Start.Y, road.End.X - road.Start.X),
                    new Vector2(0f, (float)_texture.Height / 2),
                    new Vector2(Vector2.Distance(road.Start, road.End), 1f),
                    SpriteEffects.None,
                    0f
                );
            }
        }

        public void Draw()
        {
            //DrawGrid();

            DrawRoads();

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
            const float movementSpeed = 500;
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

