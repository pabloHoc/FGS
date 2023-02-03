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

        public bool HandleInput { get; set; } = true;

        private readonly Vector2 _viewportCenter;

        private Vector2 _lastMousePosition;

        private bool _isPanning = false;

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

            _viewportCenter = new Vector2(_graphicsDevice.Viewport.Width / 2, _graphicsDevice.Viewport.Height / 2);

            _eventManager.OnRegionSecondaryAction += HandleRegionRightClick;
        }

        private void HandleRegionRightClick(object sender, Region region)
        {
            if (_eventManager.SelectedAgent != null)
            {
                _serviceProvider.Dispatcher.Dispatch(new MoveEntity
                {
                    EntityId = _eventManager.SelectedAgent.Id,
                    EntityType = EntityType.Agent,
                    RegionId = region.Id
                });
            }
        }

        public void Initialize()
        {
            var regions = _serviceProvider.GlobalState.World.Regions;

            foreach (var region in regions)
            {
                _locations.Add(new MapLocation(region, _eventManager, _graphicsDevice, _spriteBatch, _camera));
                GenerateRoads(region);
            }
        }

        private void GenerateRoads(Region region)
        {
            foreach (var connectedRegion in region.ConnectedTo)
            {
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
            if (HandleInput)
            {
                var mouseState = MouseExtended.GetState();

                if (!_isPanning && mouseState.IsButtonDown(MouseButton.Left))
                {
                    _lastMousePosition = mouseState.Position.ToVector2();
                    _isPanning = true;
                }
                if (mouseState.IsButtonUp(MouseButton.Left))
                {
                    _isPanning = false;
                }

                UpdateCamera(gameTime);

                foreach (var location in _locations)
                {
                    location.Update();
                }   
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

            var zoomCenter = _camera.ScreenToWorld(mouseState.Position.ToVector2());
            var pastZoom = _camera.Zoom;

            if (_currentMouseWheelValue > previousMouseWheelValue)
            {
                _camera.ZoomIn(1 / 12f);
                // Move towards zoom position
                _camera.Position += (zoomCenter - _camera.Origin - _camera.Position) * ((_camera.Zoom - pastZoom) / _camera.Zoom);
            }

            if (_currentMouseWheelValue < previousMouseWheelValue)
            {
                _camera.ZoomOut(1 / 12f);
                // Move towards zoom position
                _camera.Position += (zoomCenter - _camera.Origin - _camera.Position) * ((_camera.Zoom - pastZoom) / _camera.Zoom);
            }
        }

        private Vector2 GetCameraMovementDirection()
        {
            var movementDirection = Vector2.Zero;
            var state = Keyboard.GetState();
            var mouseState = MouseExtended.GetState();
            var mouseBorderArea = 20;

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

            // Mouse is in border
            // TODO: improve movement (inercia, acceleration, etc)
            if (mouseState.Y > _graphicsDevice.Viewport.Height - mouseBorderArea ||
                mouseState.Y < mouseBorderArea ||
                mouseState.X < mouseBorderArea ||
                mouseState.X > _graphicsDevice.Viewport.Width - mouseBorderArea
                )
            {
                var vectorDistance = mouseState.Position.ToVector2() - _viewportCenter;
                var vectorDirection = vectorDistance / vectorDistance.Length();
                movementDirection += vectorDirection;
            }

            // TODO: improve panning (clamping, limit movement, etc)
            if (_isPanning)
            {
                var vectorDistance = mouseState.Position.ToVector2() - _lastMousePosition;
                movementDirection += Vector2.Clamp(vectorDistance, new Vector2(-2, -2), new Vector2(2, 2));
            }

            return movementDirection;
        }
    }
}

